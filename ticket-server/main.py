import base64
import datetime
import hashlib
import hmac

from flask import Flask, request, jsonify
from sqlalchemy import create_engine, Column, Integer, String, DateTime, ForeignKey
from sqlalchemy.orm import sessionmaker, relationship, declarative_base

# the timezone of the server, which is Canada/Eastern in this case.
# We should be using UTC everywhere, but I don't have the time to allow multiple time zone in the admin app
server_timezone = datetime.timezone(datetime.timedelta(hours=-5))

app = Flask(__name__)

# --- Database Setup ---
# Connection string: note that the database name "best-tickets" is used as provided.
DATABASE_URI = 'mysql+pymysql://root:@localhost:3306/best-tickets'
engine = create_engine(DATABASE_URI, echo=True)
Session = sessionmaker(bind=engine)
Base = declarative_base()

# Hospital table model
class Hospital(Base):
    __tablename__ = 'Hospital'
    id = Column('Id', Integer, primary_key=True)
    name = Column('Name', String(255))
    address = Column('Address', String(255))
    logo = Column('Logo', String)  # Stored as a base64 string
    password = Column('Password', String)  # Stored as a hash

# Ticket table model
class Ticket(Base):
    __tablename__ = 'Ticket'
    id = Column('Id', Integer, primary_key=True, autoincrement=True)
    hospital_id = Column('HospitalId', Integer, ForeignKey('Hospital.Id'))
    creation_time = Column('CreationTime', DateTime, default=datetime.datetime.now(server_timezone))
    payment_time = Column('PaymentTime', DateTime, nullable=True)
    departure_time = Column('DepartureTime', DateTime, nullable=True)
    ticket_payment_id = Column('TicketPaymentId', Integer, nullable=True)
    ticket_number = Column('TicketNumber', String, nullable=True)

    hospital = relationship("Hospital")


# --- Utility Functions ---
def verify_password(input_password, hashed_password):
    """
    Convert the C# password verifier to Python.
    The stored hashed_password is expected to be a base64-encoded string
    where the first 16 bytes are the salt and the next 20 bytes the hash.
    """
    try:
        hash_bytes = base64.b64decode(hashed_password)
    except Exception:
        return False
    salt = hash_bytes[:16]
    stored_hash = hash_bytes[16:36]
    computed_hash = hashlib.pbkdf2_hmac('sha1', input_password.encode('utf-8'),
                                        salt, 100000, dklen=20)
    return hmac.compare_digest(stored_hash, computed_hash)

def get_ticket_number(ticket :Ticket):
    """
    Returns a unique code for the ticket.
    Code format : XXX-XXX-XXX
    """
    fullhash :String = hashlib.sha256(f"{ticket.creation_time}{ticket.hospital_id}".encode()).hexdigest().upper()
    return f"{fullhash[:3]}-{fullhash[3:6]}-{fullhash[6:9]}"

# --- Routes ---
@app.route('/create_ticket', methods=['POST'])
def create_ticket():
    """
    Creates a new ticket.
      - Requires JSON input containing 'hospital_id' and 'password'.
      - The new ticket is inserted with:
          • creation_time set to the current time,
          • payment_time, departure_time, ticket_payment_id set to null.
      - The returned JSON includes the ticket details plus hospital information.
    """
    data = request.get_json()
    if not data or 'hospital_id' not in data or 'password' not in data:
        return jsonify({'error': 'Missing hospital_id or password'}), 400

    hospital_id = data['hospital_id']
    input_password = data['password']

    session = Session()
    hospital = session.query(Hospital).filter(Hospital.id == hospital_id).first()
    if not hospital:
        session.close()
        return jsonify({'error': 'Invalid hospital'}), 401

    if not verify_password(input_password, hospital.password):
        session.close()
        return jsonify({'error': 'Unauthorized'}), 401

    # Create and insert a new ticket
    new_ticket = Ticket(
        hospital_id=hospital_id,
        creation_time=datetime.datetime.now(server_timezone),
        payment_time=None,
        departure_time=None,
        ticket_payment_id=None
    )
    new_ticket.ticket_number = get_ticket_number(new_ticket)
    session.add(new_ticket)
    session.commit()
    session.refresh(new_ticket)

    # Return ticket details and hospital information
    ticket_data = {
        'Id': new_ticket.id,
        'HospitalId': new_ticket.hospital_id,
        'CreationTime': new_ticket.creation_time.isoformat(),
        'PaymentTime': new_ticket.payment_time.isoformat() if new_ticket.payment_time else None,
        'DepartureTime': new_ticket.departure_time.isoformat() if new_ticket.departure_time else None,
        'TicketPaymentId': new_ticket.ticket_payment_id,
        'TicketNumber': new_ticket.ticket_number,
        'Hospital': {
            'Id': hospital.id,
            'Name': hospital.name,
            'Address': hospital.address,
            'Logo': hospital.logo
        }
    }

    
    session.close()
    return jsonify(ticket_data), 200
    

@app.route('/validate_ticket', methods=['POST'])
def validate_ticket():
    """
    Validates an existing ticket.
      - Requires JSON input containing 'hospital_id', 'password', and 'ticket_number'.
      - Verifies that:
          • The ticket exists (and belongs to the hospital),
          • The ticket's payment_time is set and is within the last 30 minutes.
      - If valid, updates departure_time to current time and returns {'valid': True}.
      - Otherwise returns a response indicating invalidity.
    """
    data = request.get_json()
    if not data or 'hospital_id' not in data or 'password' not in data or 'ticket_number' not in data:
        return jsonify({'error': 'Missing hospital_id, password, or ticket_number'}), 400

    hospital_id = data['hospital_id']
    input_password = data['password']
    ticket_number = data['ticket_number']

    session = Session()
    hospital = session.query(Hospital).filter(Hospital.id == hospital_id).first()
    if not hospital:
        session.close()
        return jsonify({'error': 'Invalid hospital'}), 401

    if not verify_password(input_password, hospital.password):
        session.close()
        return jsonify({'error': 'Unauthorized'}), 401

    ticket = session.query(Ticket).filter(Ticket.ticket_number == ticket_number).first()
    if not ticket:
        session.close()
        return jsonify({'error': 'Ticket not found'}), 404

    # Check that the ticket has been paid 
    if not ticket.payment_time:
        session.close()
        return jsonify({'valid': False, 'error': 'Ticket not paid'}), 400
    #and that payment was made less than 30 minutes ago.
    now = datetime.datetime.now(server_timezone)
    if not (ticket.creation_time < ticket.payment_time and
            (now - ticket.payment_time.replace(tzinfo=server_timezone)) <= datetime.timedelta(minutes=30)):
        session.close()
        return jsonify({'valid': False, 'error': 'Ticket payment expired'}), 400

    # Valid ticket: update departure_time
    ticket.departure_time = now
    session.commit()
    session.close()
    return jsonify({'valid': True}), 200

@app.route('/list_hospitals', methods=['GET'])
def list_hospitals():
    """
    Lists all hospitals and their IDs.
    No authentication required.
    Returns a JSON array of objects with hospital id and name.
    """
    session = Session()
    hospitals = session.query(Hospital).all()
    
    hospital_list = [
        {
            'id': hospital.id,
            'name': hospital.name
        }
        for hospital in hospitals
    ]
    
    session.close()
    return jsonify(hospital_list), 200

@app.route('/verify_credentials', methods=['POST'])
def verify_credentials():
    """
    Verifies if the provided hospital credentials are correct.
    Requires JSON input containing 'hospital_id' and 'password'.
    Returns {'valid': True} if credentials are valid, {'valid': False} otherwise.
    """
    data = request.get_json()
    if not data or 'hospital_id' not in data or 'password' not in data:
        return jsonify({'error': 'Missing hospital_id or password'}), 400

    hospital_id = data['hospital_id']
    input_password = data['password']

    session = Session()
    hospital = session.query(Hospital).filter(Hospital.id == hospital_id).first()
    
    if not hospital:
        session.close()
        return jsonify({'valid': False, 'error': 'Hospital not found'}), 200

    is_valid = verify_password(input_password, hospital.password)
    session.close()
    
    return jsonify({'valid': is_valid}), 200

if __name__ == '__main__':
    # In production, ensure to disable debug mode and serve via a proper WSGI server.
    # app.run(debug=True)
    # for prod :
    import waitress
    waitress.serve(app, port=5000)
