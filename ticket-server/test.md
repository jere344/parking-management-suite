Here are a bunch of tests for your Flask API with corresponding `curl` commands that you can run on Windows (PowerShell or Command Prompt). Since Windows doesn't handle single quotes well, I'll use double quotes for JSON payloads.

---

### **1. Test: Create a Ticket (Valid Credentials)**
📌 **Expected:** Returns a JSON object with a new ticket and hospital details.

```powershell
curl -X POST http://127.0.0.1:5000/create_ticket ^
     -H "Content-Type: application/json" ^
     -d "{ \"hospital_id\": 1, \"password\": \"correct-password\" }"
```

---

### **2. Test: Create a Ticket (Invalid Password)**
📌 **Expected:** Returns `401 Unauthorized`.

```powershell
curl -X POST http://127.0.0.1:5000/create_ticket ^
     -H "Content-Type: application/json" ^
     -d "{ \"hospital_id\": 1, \"password\": \"wrong-password\" }"
```

---

### **3. Test: Create a Ticket (Non-Existent Hospital)**
📌 **Expected:** Returns `401 Invalid hospital`.

```powershell
curl -X POST http://127.0.0.1:5000/create_ticket ^
     -H "Content-Type: application/json" ^
     -d "{ \"hospital_id\": 999, \"password\": \"correct-password\" }"
```

---

### **4. Test: Validate Ticket (Valid and Recently Paid)**
📌 **Expected:** Returns `{"valid": true}` if the ticket was paid within the last 30 minutes.

```powershell
curl -X POST http://127.0.0.1:5000/validate_ticket ^
     -H "Content-Type: application/json" ^
     -d "{ \"hospital_id\": 1, \"password\": \"correct-password\", \"ticket_id\": 1 }"
```

---

### **5. Test: Validate Ticket (Invalid Password)**
📌 **Expected:** Returns `401 Unauthorized`.

```powershell
curl -X POST http://127.0.0.1:5000/validate_ticket ^
     -H "Content-Type: application/json" ^
     -d "{ \"hospital_id\": 1, \"password\": \"wrong-password\", \"ticket_id\": 123 }"
```

---

### **6. Test: Validate Ticket (Non-Existent Ticket)**
📌 **Expected:** Returns `404 Ticket not found`.

```powershell
curl -X POST http://127.0.0.1:5000/validate_ticket ^
     -H "Content-Type: application/json" ^
     -d "{ \"hospital_id\": 1, \"password\": \"correct-password\", \"ticket_id\": 99999 }"
```

---

### **7. Test: Validate Ticket (Unpaid Ticket)**
📌 **Expected:** Returns `{"valid": false, "error": "Ticket not paid"}`.

```powershell
curl -X POST http://127.0.0.1:5000/validate_ticket ^
     -H "Content-Type: application/json" ^
     -d "{ \"hospital_id\": 1, \"password\": \"correct-password\", \"ticket_id\": 124 }"
```

---

### **8. Test: Validate Ticket (Payment Expired - Paid Over 30 Minutes Ago)**
📌 **Expected:** Returns `{"valid": false, "error": "Ticket payment expired"}`.

```powershell
curl -X POST http://127.0.0.1:5000/validate_ticket ^
     -H "Content-Type: application/json" ^
     -d "{ \"hospital_id\": 1, \"password\": \"correct-password\", \"ticket_id\": 125 }"
```

---

### **9. Test: Missing Required Fields in Create Ticket**
📌 **Expected:** Returns `400 Missing hospital_id or password`.

```powershell
curl -X POST http://127.0.0.1:5000/create_ticket ^
     -H "Content-Type: application/json" ^
     -d "{ \"hospital_id\": 1 }"
```

---

### **10. Test: Missing Required Fields in Validate Ticket**
📌 **Expected:** Returns `400 Missing hospital_id, password, or ticket_id`.

```powershell
curl -X POST http://127.0.0.1:5000/validate_ticket ^
     -H "Content-Type: application/json" ^
     -d "{ \"hospital_id\": 1, \"password\": \"correct-password\" }"
```

---

These should cover the main scenarios, including valid and invalid authentication, missing fields, and different ticket validation cases. Let me know if you need any modifications! 🚀