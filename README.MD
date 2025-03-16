# Best Tickets - Parking Management System

## Project Overview

Best Tickets is a comprehensive parking management system designed primarily for hospitals and other facilities requiring efficient parking ticket handling. The system enables ticket creation, payment processing, subscription management, gate control, and administrative oversight through a suite of interconnected applications.

## System Architecture

The system consists of four main components:

1. **Admin Software** - A central management application for system administrators
2. **Payment Terminal** - Customer-facing interface for processing ticket payments
3. **Gates Software** - Controls entry and exit gates
4. **Ticket Library** - Shared component library
5. **Ticket Server** - API backend handling data operations

The system uses a MySQL database for data storage, with connection strings configured in appsettings.json files.

## Components

### Admin Software

A desktop WPF application built with .NET 6.0 that provides administrators with:

- Dashboard for parking data visualization using LiveCharts
- Ticket management and reporting
- Hospital registration and configuration
- User management
- Discount code creation and management
- Subscription tier configuration
- Price bracket management
- Tax rate configuration
- Gate monitoring and control

### Payment Terminal

A customer-facing WPF application that allows users to:

- Input ticket information
- Process payments for single-use tickets
- Manage subscription cards and payments
- Print receipts and tickets

### Gates Software

A .NET 9.0 WPF application that:

- Controls physical entry and exit gates
- Validates tickets and subscriptions
- Interfaces with the server API to authenticate access
- Provides real-time gate status monitoring

### Ticket Library

A shared .NET library that contains:

- Core data models and entities
- PDF generation using QuestPDF
- Barcode generation with ZXing.Net
- Shared business logic

### Ticket Server

A Python Flask API backend that provides:

- Authentication and authorization
- Ticket creation and validation
- Gate signal management
- Hospital listing and verification
- RESTful endpoints for client applications

## Setup Instructions

### Prerequisites

- .NET 6.0 SDK (for admin software, payment terminal, ticket library)
- .NET 9.0 SDK (for gates software)
- Python 3.x with pip (for ticket server)
- MySQL Server 5.7+ or compatible
- Visual Studio 2022 or later (recommended for development)

### Database Setup

1. Create a MySQL database named `best-tickets`
2. Configure connection strings in:
   - `admin-software/appsettings.json`
   - `payment-terminal/appsettings.json`
   - Update `DATABASE_URI` in `ticket-server/main.py`
3. Navigate to the admin-software directory and apply database migrations:
   ```
   dotnet ef database update
   ```
   This will create all required database tables and schema.

### Server Setup

1. Navigate to the ticket-server directory
2. Install Python dependencies:
   ```
   python -m pip install -r requirements.txt
   ```
3. Run the server:
   ```
   python main.py
   ```
4. The server will be available at http://localhost:5000
5. The server port can be changed at the end of `main.py`

### Client Applications Setup

1. Open the solution file `best-tickets.sln` in Visual Studio
2. Restore NuGet packages
3. Build the solution
4. Run the desired application(s)

### Gates Software Setup

#### Prerequisites
- .NET 9.0 SDK or later
- Visual Studio 2022 or compatible IDE
- Network connectivity to the Ticket Server API
- Physical gate hardware (or simulation software) compatible with the system

#### Installation Steps
1. Navigate to the `gates-software` directory
2. Build the project using Visual Studio or via command line:
   ```
   dotnet build
   ```
3. Run the application:
   ```
   dotnet run
   ```
   
   Or build a standalone executable:
   ```
   dotnet publish -c Release -r win-x64 --self-contained true
   ```

#### Configuration
1. **Environment Variables**: 
   - Create or update the `.env` file in the application directory with:
     ```
     API_BASE_URL=http://your-server-address:port
     ```

2. **First-Time Setup**:
   - On first launch, the application will present a setup wizard
   - Select your hospital from the dropdown list
   - Enter the hospital gateway password provided by your system administrator
   - Choose the gate type (entry or exit) depending on the installation location
   - The configuration will be stored in `settings.json` and loaded automatically on subsequent launches

3. **Gate Hardware Integration**:
   - Connect the gate control hardware to the computer running the software
   - Hardware-specific configuration may be required depending on your gate controller model
   - The application will automatically manage the gate opening signals

#### Usage
1. **Entry Gate Mode**:
   - The interface provides a "Generate Ticket" button
   - When pressed, a new ticket is created, the gate opens automatically, and a PDF ticket is generated for printing
   
2. **Exit Gate Mode**:
   - The interface allows inputting a ticket number (format: XXX-XXX-XXX)
   - The system validates if the ticket has been paid
   - If valid, the gate opens automatically
   - Invalid tickets will display an error message

#### Gate Status Monitoring
- The application displays a color-coded status indicator (green for open, red for closed)
- The gate status is checked every 5 seconds
- Gates can be opened automatically by the system for 10 seconds when a valid action is performed

#### Troubleshooting
- **Connection Issues**: Verify the API_BASE_URL in the .env file and ensure network connectivity
- **Authentication Failures**: The setup wizard will prompt for the correct password
- **Gate Control Problems**: Verify physical connections to gate hardware and permissions

#### Security Considerations
- Keep the gateway password secure
- If a breach is suspected, the administrator can change the password from the admin software
- Limit physical access to the computer running the gates software
- Regularly update credentials according to your organization's security policy

## Key Features

### Ticket Management

- QR-code based ticket generation
- Multiple payment options
- Time-based pricing with customizable brackets

### Subscription System

- Subscription tiers with different durations and pricing
- Card-based subscriber identification
- Usage tracking and limitations

### Gate Control

- Automated entry gate management
- Exit validation based on payment status
- Real-time gate monitoring and manual control

### Reporting & Analytics

- Financial reporting with revenue breakdown
- Usage statistics and trends visualization
- Tax calculation and reporting

### Administration

- Multi-language support (English and French)
- Customizable theme and appearance
- Secure user authentication
- Hospital-specific configuration

## Initial Login

The system is initialized with a default administrator account:
- **Username**: admin
- **Password**: admin

⚠️ **IMPORTANT**: For security reasons, you should immediately delete this default account or change its credentials after logging in for the first time.

## Technologies Used

### Frontend
- WPF with .NET 6.0 and .NET 9.0
- MaterialDesignThemes for UI
- CommunityToolkit.Mvvm for MVVM pattern
- LiveCharts for data visualization

### Backend
- Flask (Python) for API
- Entity Framework Core for ORM
- SQLAlchemy (Python)

### Database
- MySQL

### Others
- QuestPDF for document generation
- ZXing.Net for barcode handling
- PBKDF2 for password security

## License

Version 2.0, January 2004, voir le fichier LICENSE pour plus de détails.

## Authors

Jeremy Guerin - https://github.com/jere344

---

*This README was last updated on March 2025*