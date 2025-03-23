# Payment Terminal User Guide

## Introduction
The Best Tickets Payment Terminal is an application for processing parking ticket payments at hospitals and managing subscription services.

## Getting Started
Launch the application to access the main interface where you can select from available payment options.

## Main Interface
From the home screen, you can:
- Créditer un ticket unique (Process single ticket payment)
- Créditer un ticket avec un abonnement (Process ticket with subscription)
- Acheter un abonnement (Buy a subscription)

## Ticket Payment Process

### Entering Ticket Information
1. Select "Créditer un ticket unique" from the home screen
2. Enter your ticket number in the format XXX-XXX-XXX
   - You can manually type the number or use the "Scan" button to simulate scanning a ticket
3. Click "Suivant" (Next) to proceed

### Payment Methods
The system supports payment by credit card ("Carte bancaire").

### Payment Details
The payment screen displays:
- Ticket information (number and creation time)
- Hospital information
- Parking duration
- Payment breakdown including:
  - Base price before tax
  - Any applicable reduction codes
  - Taxes
  - Total amount

### Applying Reduction Codes
You can apply a reduction code:
1. Enter your code in the "Code de Réduction" field
2. Click "Appliquer" (Apply)
3. If valid, a discount will be applied to your payment

### Completing a Transaction
1. Click "Procéder au paiement" to complete your payment
2. Print your receipt from the confirmation screen
3. Click "Terminer" to finish the transaction and return to the home screen

## Subscription Management

### Creating a New Subscription
1. Select "Acheter un abonnement" from the home screen
2. Select your hospital from the hospital selection screen
   - Each hospital is displayed with its logo, name, and address
   - Click on the hospital card to select it
3. Choose a subscription tier from the available options
   - Each tier has a different duration and price
   - Tiers may have a maximum number of uses per day
4. Confirm your selection when prompted
5. The system will generate a unique subscription card number in XXXX-XXXX format
6. Print your subscription card

### Using a Subscription for Payment
1. Select "Créditer un ticket avec un abonnement" from the home screen
2. Enter your ticket number in the format XXX-XXX-XXX
3. Enter your subscription card number in the format XXXX-XXXX
   - You can manually type the number or use the "Scan" button to simulate scanning a card
4. The system will verify your subscription validity and usage count
5. If valid, click "Procéder au paiement" to complete the process
6. Print your ticket receipt

## Subscription Details
- Each subscription has a validity period with start and end dates
- Subscriptions have a daily usage limit (maximum number of uses per day)
- The system tracks how many times you've used your subscription each day
- You cannot use an expired subscription or one that has reached its daily usage limit
- Subscription cards include:
  - Hospital name and logo
  - Card number
  - Valid from and until dates
  - Maximum uses per day
  - Subscription price paid
  - QR code for easy scanning

## Printing Functions

### Ticket Printing
After payment completion:
1. Click "Imprimer le ticket" on the ticket print screen
2. Save the PDF file when prompted
3. Choose whether to open the PDF
4. Click "Terminer" to complete the process

You can also save tickets as PNG images by selecting the appropriate option.

### Subscription Card Printing
After completing a subscription purchase:
1. You'll be presented with the subscription card printing screen
2. Click "Imprimer la carte" to print your subscription card
3. Save the PDF file when prompted
4. You'll have the option to open the PDF immediately
5. Click "Terminer" to finish the process and return to the home screen
   - Note: You must print your subscription card before you can complete the process

## User Interface Navigation

The payment terminal features a user-friendly interface with:

- Clear navigation buttons at the bottom of most screens
- A "Retour" (Back) button to return to the previous screen
- Confirmation dialogs for important actions
- Error messages displayed clearly when issues occur
- Form fields with automatic formatting (e.g., for ticket numbers and subscription card numbers)
- Visual indicators showing the status of tickets and subscriptions

## Settings and Configuration
The application settings are stored in a settings.json file. These settings can only be modified by administrators through direct file editing:

- Language settings (English "en" or French "fr")
- Theme settings (light or dark)
- Primary color scheme
- Other system preferences

Administrator Note: To change these settings, locate the settings.json file in the application directory and modify the values manually. The application must be restarted for changes to take effect.

### Example settings.json File
```json
{
  "Language": "fr",
  "Theme": "dark",
  "PrimaryColor": "DeepPurple"
}
```

Available options:
- Language: "en" or "fr" 
- Theme: "light" or "dark"
- PrimaryColor: "Blue", "DeepPurple", "Indigo", "Teal", "Green", "Orange", "Red", or "Pink"
- /!\ Do not modify the structure of the settings.json file
- /!\ The settings are not officially supported, use at your own risk

## Troubleshooting

### Common Issues
- If a ticket is not found, verify you've entered the correct ticket number
- If a ticket shows as "already paid," contact support for assistance
- If a subscription card is invalid or expired, check the card number and validity dates
- Subscription cards that have reached their daily usage limit cannot be used until the next day

### System Requirements
- Windows operating system
- Internet connection for database access
- Printer connection for printing tickets and subscription cards

## Navigation
- Most screens have a "Retour" (Back) button to return to the previous screen
- After completing a transaction, you'll be returned to the home screen
- You can access administrative functions through the application menu

## Glossary
- **Ticket**: Parking voucher with a unique number in XXX-XXX-XXX format
- **Reduction Code**: Special code that provides a discount on parking fees (percentage-based)
- **Subscription**: Pre-paid parking plan that can be used for multiple parking sessions within its validity period
- **Card Number**: Unique identifier for subscription cards in XXXX-XXXX format
- **Price Bracket**: Pricing tier based on parking duration
- **Taxes**: Additional charges applied to the base price (such as sales tax)
- **Hospital**: Medical facility with its own parking pricing and policies
- **TicketPayment**: Record of a completed payment transaction
