# Usage Guide for BestTickets Gate Software

**Note**: For the admin panel usage gate, please refer to the [Admin Panel Usage Guide]("./admin%20panel%20usage%20guide.md").

## Table of Contents
- [Usage Guide for BestTickets Gate Software](#usage-guide-for-besttickets-gate-software)
  - [Table of Contents](#table-of-contents)
  - [Introduction](#introduction)
  - [Initial Setup](#initial-setup)
  - [Entry Gate Operation](#entry-gate-operation)
  - [Exit Gate Operation](#exit-gate-operation)
  - [Printed Ticket Format](#printed-ticket-format)
  - [Gate Status Indicator](#gate-status-indicator)
  - [Troubleshooting](#troubleshooting)
    - [Connection Issues](#connection-issues)
    - [Invalid Credentials](#invalid-credentials)
    - [Gate Not Responding](#gate-not-responding)
    - [Reset Configuration](#reset-configuration)

## Introduction
BestTickets Gate Software is designed to manage vehicle entry and exit operations at parking facilities. The software supports both entry gates (ticket generation) and exit gates (ticket validation) with a user-friendly interface.

## Initial Setup
When you first launch the application, you'll need to complete the initial setup:

1. Select your hospital/facility from the dropdown list.
2. Enter your facility password provided by your system administrator.
3. Select the gate type:
   - Choose **entry** for gates where vehicles enter the parking area
   - Choose **exit** for gates where vehicles leave the parking area
4. Click the **Save** button to store your settings.
5. The application will prompt you to restart - click OK to close and then relaunch the application.

![Initial Setup Screen](./images/initial-setup.png)

## Entry Gate Operation
If configured as an entry gate, the software provides a simple interface to generate parking tickets:

1. Click the **Generate Ticket** button when a vehicle arrives.
2. The system will:
   - Generate a unique ticket number
   - Open the gate automatically (gate indicator will turn green)
   - Present a dialog to save the ticket as a PDF file
3. Choose a location to save the PDF ticket.
4. Optionally view the ticket by clicking "Yes" when prompted.
5. Print the ticket for the driver.

The gate will automatically close after 10 seconds.

![Entry Gate Screen](./images/entry-gate.png)

## Exit Gate Operation
If configured as an exit gate, the software validates tickets from exiting vehicles:

1. Enter the ticket number in the format XXX-XXX-XXX.
   - The system will automatically add dashes as you type.
   - You can scan the ticket barcode if you have a compatible scanner.
2. Click **Validate** to verify the ticket.
3. If the ticket is valid, the gate will automatically open (gate indicator will turn green).
4. The gate will automatically close after 10 seconds.

![Exit Gate Screen](./images/exit-gate.png)

## Printed Ticket Format
When a ticket is generated at the entry gate, it includes the following information:

- Hospital/facility name and address
- Unique ticket number
- Entry date and time
- QR code for quick scanning at exit
- Payment instructions
- Ticket validity information

The ticket is printed in a compact format designed to fit in a wallet or vehicle dashboard. Users should keep the ticket safe as it will be required when exiting the facility.

![Printed Ticket Example](./images/printed-ticket.jpg)

## Gate Status Indicator
In the top-right corner of the application, you'll see a gate status indicator:

- **Green**: Gate is currently open
- **Red**: Gate is currently closed
- **Gray**: Gate status is unknown (possible connectivity issue)

The system continuously monitors the gate status and updates this indicator every 5 seconds.

## Troubleshooting

### Connection Issues
If you experience connection problems:
- Verify your internet connection
- Check that the server is operational
- Ensure your facility credentials are correct

### Invalid Credentials
If you receive an "Invalid credentials" message:
- Double-check your password
- Contact your system administrator to verify your access

### Gate Not Responding
If the gate does not open when expected:
- Check physical gate connections
- Verify the gate status indicator is updating
- Restart the application
- Contact technical support if the problem persists

### Reset Configuration
To reset your configuration and return to the setup screen:
1. Close the application
2. Delete the "settings.json" file from the application folder
3. Restart the application

For additional support, please contact your system administrator or the BestTickets support team at support@besttickets.example.com.