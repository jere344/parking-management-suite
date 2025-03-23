# Table of Contents

- [Table of Contents](#table-of-contents)
- [Usage Guide for BestTickets Admin Software](#usage-guide-for-besttickets-admin-software)
  - [Introduction](#introduction)
  - [Getting Started](#getting-started)
    - [Installation](#installation)
    - [First Login](#first-login)
    - [Interface Overview](#interface-overview)
      - [Top Navigation Bar](#top-navigation-bar)
      - [Side Navigation Drawer](#side-navigation-drawer)
      - [Main Content Area](#main-content-area)
  - [Dashboard](#dashboard)
    - [Dashboard Overview](#dashboard-overview)
    - [Statistical Summary Cards](#statistical-summary-cards)
    - [Tickets Over Time Chart](#tickets-over-time-chart)
    - [Ticket Status Distribution](#ticket-status-distribution)
    - [Revenue by Hospital Chart](#revenue-by-hospital-chart)
    - [Payment Methods Distribution](#payment-methods-distribution)
    - [Refreshing Dashboard Data](#refreshing-dashboard-data)
    - [Data Considerations](#data-considerations)
  - [User Management](#user-management)
    - [Accessing User Management](#accessing-user-management)
    - [Viewing User Accounts](#viewing-user-accounts)
    - [Filtering User Accounts](#filtering-user-accounts)
    - [Viewing User Details](#viewing-user-details)
    - [Editing User Information](#editing-user-information)
    - [Disabling/Enabling User Accounts](#disablingenabling-user-accounts)
    - [Deleting User Profile Pictures](#deleting-user-profile-pictures)
    - [Creating New User Accounts](#creating-new-user-accounts)
    - [Security Best Practices](#security-best-practices)
  - [Profile Management](#profile-management)
    - [Accessing Your Profile](#accessing-your-profile)
    - [Profile Information](#profile-information)
    - [Editing Your Profile](#editing-your-profile)
    - [Changing Your Password](#changing-your-password)
  - [Hospital Management](#hospital-management)
    - [Accessing Hospital Management](#accessing-hospital-management)
    - [Viewing Hospitals](#viewing-hospitals)
    - [Managing Hospital Passwords](#managing-hospital-passwords)
    - [Deleting Hospitals](#deleting-hospitals)
    - [Adding a New Hospital](#adding-a-new-hospital)
    - [Best Practices](#best-practices)
  - [Ticket Management](#ticket-management)
    - [Accessing Ticket Management](#accessing-ticket-management)
    - [Viewing Tickets](#viewing-tickets)
    - [Navigating Ticket Pages](#navigating-ticket-pages)
    - [Searching and Filtering Tickets](#searching-and-filtering-tickets)
    - [Sorting Tickets](#sorting-tickets)
    - [Viewing Ticket Payment Details](#viewing-ticket-payment-details)
    - [Managing Ticket Status](#managing-ticket-status)
    - [Exporting Ticket Information](#exporting-ticket-information)
    - [Best Practices](#best-practices-1)
    - [Data Considerations](#data-considerations-1)
  - [Subscription Management](#subscription-management)
    - [Accessing Subscription Management](#accessing-subscription-management)
    - [Viewing Subscriptions](#viewing-subscriptions)
    - [Understanding Subscription Status](#understanding-subscription-status)
    - [Exporting Subscription Cards](#exporting-subscription-cards)
    - [Best Practices](#best-practices-2)
    - [Data Considerations](#data-considerations-2)
    - [Active Subscriptions](#active-subscriptions)
    - [Subscription Tiers](#subscription-tiers)
    - [Refreshing Subscription Data](#refreshing-subscription-data)
  - [Discount Codes](#discount-codes)
    - [Accessing Discount Codes Management](#accessing-discount-codes-management)
    - [Viewing Discount Codes](#viewing-discount-codes)
    - [Managing Discount Codes](#managing-discount-codes)
      - [Filtering Discount Codes](#filtering-discount-codes)
      - [Enabling or Disabling Codes](#enabling-or-disabling-codes)
      - [Deleting Discount Codes](#deleting-discount-codes)
      - [Copying Discount Codes](#copying-discount-codes)
    - [Creating New Discount Codes](#creating-new-discount-codes)
    - [Best Practices](#best-practices-3)
    - [Data Considerations](#data-considerations-3)
  - [Price Brackets](#price-brackets)
    - [Accessing Price Brackets Management](#accessing-price-brackets-management)
    - [Viewing Price Brackets](#viewing-price-brackets)
    - [Understanding Price Brackets](#understanding-price-brackets)
    - [Managing Price Brackets](#managing-price-brackets)
      - [Deleting Price Brackets](#deleting-price-brackets)
      - [Copying Price Brackets](#copying-price-brackets)
    - [Creating New Price Brackets](#creating-new-price-brackets)
    - [Global vs. Hospital-Specific Price Brackets](#global-vs-hospital-specific-price-brackets)
    - [Best Practices](#best-practices-4)
    - [Data Considerations](#data-considerations-4)
  - [Gate Control](#gate-control)
    - [Accessing Gate Control Management](#accessing-gate-control-management)
    - [Understanding Gate Status](#understanding-gate-status)
    - [Opening Gates](#opening-gates)
    - [Canceling Gate Signals](#canceling-gate-signals)
    - [Active Signals](#active-signals)
    - [Security Considerations](#security-considerations)
    - [Best Practices](#best-practices-5)
    - [Technical Information](#technical-information)
  - [Tax Management](#tax-management)
    - [Accessing Tax Management](#accessing-tax-management)
    - [Viewing Tax Rates](#viewing-tax-rates)
    - [Managing Tax Rates](#managing-tax-rates)
      - [Deleting Tax Rates](#deleting-tax-rates)
      - [Copying Tax Rates](#copying-tax-rates)
    - [Creating New Tax Rates](#creating-new-tax-rates)
    - [Tax Application](#tax-application)
    - [Best Practices](#best-practices-6)
    - [Data Considerations](#data-considerations-5)
  - [Settings](#settings)
    - [Accessing Settings](#accessing-settings)
    - [Appearance Settings](#appearance-settings)
      - [Language Selection](#language-selection)
      - [Theme Selection](#theme-selection)
      - [Primary Color](#primary-color)
    - [Settings Persistence](#settings-persistence)
    - [Best Practices](#best-practices-7)
    - [Technical Information](#technical-information-1)
  - [Troubleshooting](#troubleshooting)
    - [Common Issues and Solutions](#common-issues-and-solutions)
      - [Application Won't Start](#application-wont-start)
      - [Database Connection Failures](#database-connection-failures)
      - [Login Problems](#login-problems)
      - [Missing Menu Items](#missing-menu-items)
      - [Gate Control Not Working](#gate-control-not-working)
      - [Tax Calculations Incorrect](#tax-calculations-incorrect)
      - [Settings Not Saving](#settings-not-saving)
      - [Subscription Card Export Fails](#subscription-card-export-fails)
      - [Price Brackets Not Applied Correctly](#price-brackets-not-applied-correctly)
      - [Dashboard Charts Not Loading](#dashboard-charts-not-loading)
    - [Getting Additional Help](#getting-additional-help)
  - [Frequently Asked Questions](#frequently-asked-questions)
    - [General Application Questions](#general-application-questions)
      - [Q: What is BestTickets Admin Software?](#q-what-is-besttickets-admin-software)
      - [Q: How often is the software updated?](#q-how-often-is-the-software-updated)
      - [Q: Can multiple administrators use the system simultaneously?](#q-can-multiple-administrators-use-the-system-simultaneously)
      - [Q: Is there a mobile version of the admin software?](#q-is-there-a-mobile-version-of-the-admin-software)
    - [Technical Questions](#technical-questions)
      - [Q: What are the minimum system requirements?](#q-what-are-the-minimum-system-requirements)
      - [Q: Does the system work offline?](#q-does-the-system-work-offline)
      - [Q: How do I update to the latest version?](#q-how-do-i-update-to-the-latest-version)
      - [Q: Can I run the software on Mac or Linux?](#q-can-i-run-the-software-on-mac-or-linux)
    - [User Management Questions](#user-management-questions)
      - [Q: Can I recover a forgotten password?](#q-can-i-recover-a-forgotten-password)
      - [Q: Why can't I delete a user account?](#q-why-cant-i-delete-a-user-account)
      - [Q: What happens to tickets processed by a disabled user?](#q-what-happens-to-tickets-processed-by-a-disabled-user)
      - [Q: How do I assign different permission levels to users?](#q-how-do-i-assign-different-permission-levels-to-users)
    - [Ticket Management Questions](#ticket-management-questions)
      - [Q: Can I edit a ticket after it's been created?](#q-can-i-edit-a-ticket-after-its-been-created)
      - [Q: How long are tickets stored in the system?](#q-how-long-are-tickets-stored-in-the-system)
      - [Q: What should I do if a customer claims to have paid but the ticket shows as unpaid?](#q-what-should-i-do-if-a-customer-claims-to-have-paid-but-the-ticket-shows-as-unpaid)
      - [Q: Can I issue refunds through the admin software?](#q-can-i-issue-refunds-through-the-admin-software)
    - [Subscription Management Questions](#subscription-management-questions)
      - [Q: How are subscription fees calculated?](#q-how-are-subscription-fees-calculated)
      - [Q: Can customers have multiple active subscriptions?](#q-can-customers-have-multiple-active-subscriptions)
      - [Q: What happens when a subscription expires?](#q-what-happens-when-a-subscription-expires)
      - [Q: Can I extend an existing subscription?](#q-can-i-extend-an-existing-subscription)
    - [Price Bracket Questions](#price-bracket-questions)
      - [Q: Do price changes affect existing tickets?](#q-do-price-changes-affect-existing-tickets)
      - [Q: Can I set different prices for weekdays and weekends?](#q-can-i-set-different-prices-for-weekdays-and-weekends)
      - [Q: How do I implement a free parking period?](#q-how-do-i-implement-a-free-parking-period)
    - [Gate Control Questions](#gate-control-questions)
      - [Q: What happens if I close the admin software while a gate is open?](#q-what-happens-if-i-close-the-admin-software-while-a-gate-is-open)
      - [Q: Can I schedule gates to open automatically at certain times?](#q-can-i-schedule-gates-to-open-automatically-at-certain-times)
      - [Q: Is there a limit to how many gates I can control?](#q-is-there-a-limit-to-how-many-gates-i-can-control)
      - [Q: Can I see a log of gate operations?](#q-can-i-see-a-log-of-gate-operations)
    - [Reporting and Dashboard Questions](#reporting-and-dashboard-questions)
      - [Q: How current is the dashboard data?](#q-how-current-is-the-dashboard-data)
      - [Q: Can I export dashboard data to Excel?](#q-can-i-export-dashboard-data-to-excel)
      - [Q: Why doesn't my hospital appear in the Revenue by Hospital chart?](#q-why-doesnt-my-hospital-appear-in-the-revenue-by-hospital-chart)
      - [Q: How can I see data for a specific time period?](#q-how-can-i-see-data-for-a-specific-time-period)
    - [Financial Questions](#financial-questions)
      - [Q: How are taxes calculated?](#q-how-are-taxes-calculated)
      - [Q: Can I issue a ticket without collecting payment?](#q-can-i-issue-a-ticket-without-collecting-payment)
      - [Q: Does the system support multiple currencies?](#q-does-the-system-support-multiple-currencies)
      - [Q: How can I reconcile daily payments?](#q-how-can-i-reconcile-daily-payments)
- [Usage Guide for BestTickets Gate Software](#usage-guide-for-besttickets-gate-software)
  - [Introduction](#introduction-1)
  - [Initial Setup](#initial-setup)
  - [Entry Gate Operation](#entry-gate-operation)
  - [Exit Gate Operation](#exit-gate-operation)
  - [Printed Ticket Format](#printed-ticket-format)
  - [Gate Status Indicator](#gate-status-indicator)
  - [Troubleshooting](#troubleshooting-1)
    - [Connection Issues](#connection-issues)
    - [Invalid Credentials](#invalid-credentials)
    - [Gate Not Responding](#gate-not-responding)
    - [Reset Configuration](#reset-configuration)
- [Payment Terminal User Guide](#payment-terminal-user-guide)
  - [Introduction](#introduction-2)
  - [Getting Started](#getting-started-1)
  - [Main Interface](#main-interface)
  - [Ticket Payment Process](#ticket-payment-process)
    - [Entering Ticket Information](#entering-ticket-information)
    - [Payment Methods](#payment-methods)
    - [Payment Details](#payment-details)
    - [Applying Reduction Codes](#applying-reduction-codes)
    - [Completing a Transaction](#completing-a-transaction)
  - [Subscription Management](#subscription-management-1)
    - [Creating a New Subscription](#creating-a-new-subscription)
    - [Using a Subscription for Payment](#using-a-subscription-for-payment)
  - [Subscription Details](#subscription-details)
  - [Printing Functions](#printing-functions)
    - [Ticket Printing](#ticket-printing)
    - [Subscription Card Printing](#subscription-card-printing)
  - [User Interface Navigation](#user-interface-navigation)
  - [Settings and Configuration](#settings-and-configuration)
    - [Example settings.json File](#example-settingsjson-file)
  - [Troubleshooting](#troubleshooting-2)
    - [Common Issues](#common-issues)
    - [System Requirements](#system-requirements)
  - [Navigation](#navigation)



# Usage Guide for BestTickets Admin Software

## Introduction

Welcome to the BestTickets Admin Software Usage Guide. This document is designed to help you navigate and leverage the comprehensive features of the BestTickets Admin Software, ensuring smooth operation and management of hospital parking facilities. Whether you are new to the system or a seasoned administrator, this guide will walk you through essential tasks, including user management, ticket processing, gate control, subscription handling, and financial reporting.

Within this guide, you'll find clear instructions and best practices to maximize system efficiency while maintaining top security standards. Each section is crafted to assist you in mastering the interface, performing key operations, and troubleshooting common issues. By following the step-by-step directions provided, you can confidently manage daily tasks and optimize the overall user experience.

This document is the usage guide of the Admin Software part of the BestTickets software suite.

## Getting Started

### Installation

_see [Installation Guide](./installation%20guide.md)_

### First Login

When logging into the BestTickets Admin Software for the first time:

1. **Access the login page**

    - Enter the URL provided by your system administrator or launch the desktop application
    - You'll see the login interface with fields for email and password

2. **Enter your credentials**

    - Use the email address provided by your administrator
    - Enter the temporary password that was provided to you
    - Check the "Remember Me" box if you want to stay logged in on this device
    - Click the "Login" button

3. **Change your temporary password**

    - Upon first login, it's strongly recommended to change your password immediately
    - Navigate to your Profile section using the icon in the top navigation bar
    - In the Profile page, locate the password change section
    - Enter your current (temporary) password
    - Enter and confirm your new password
    - Click "Change Password"
    - You'll receive a confirmation message when successful

4. **Complete your profile**

    - Update your phone number if necessary
    - Upload a profile picture by clicking the upload button
    - The system will automatically resize and optimize your image

5. **Security recommendations**
    - Choose a strong password with at least 8 characters
    - Include uppercase letters, lowercase letters, numbers, and special characters
    - Don't reuse passwords from other systems
    - Never share your password with others

### Interface Overview

The BestTickets Admin Software interface consists of three main components:

#### Top Navigation Bar

The top bar provides quick access to essential navigation and account functions:

-   **Menu Button**: Opens the side navigation drawer
-   **Navigation Controls**: Back, Forward, and Home buttons for moving between views
-   **Application Title**: Displays "BestTickets" (clicking opens the GitHub repository)
-   **Settings**: Access application configuration
-   **Profile**: View and edit your user profile
-   **Logout**: Sign out of the application

#### Side Navigation Drawer

The left drawer contains the main navigation menu, accessed by clicking the menu button in the top-left corner. This menu adapts to your permission level, showing only the options you have access to:

-   Dashboard: Overview of system performance and statistics
-   User Management: Manage user accounts and permissions
-   Profile: Access your personal profile settings
-   Hospital Management: Add and configure hospital facilities
-   Ticket Management: View and process parking tickets
-   Subscription Management: Configure tiers and view active subscriptions
-   Discount Codes: Create and manage promotional offers
-   Price Brackets: Set time-based pricing structures
-   Gate Control: Control parking gates from a distance
-   Tax Management: Configure tax rates and generate reports
-   Settings: Application configuration options

#### Main Content Area

This central area displays the currently selected view, where you'll perform most of your work. The content changes based on the function selected from the navigation drawer.

**Navigation Tips:**

-   Use the Back and Forward buttons to move through your viewing history
-   The Home button returns you to your default landing page
-   Your available menu options depend on your user permission level

## Dashboard

The Dashboard provides a comprehensive overview of system performance, statistics, and key metrics. This central hub helps administrators monitor ticket activity, revenue generation, and subscription status at a glance.

### Dashboard Overview

The Dashboard is divided into several sections:

-   Statistical Summary Cards
-   Tickets Over Time Chart
-   Ticket Status Distribution
-   Revenue by Hospital
-   Payment Methods Distribution

### Statistical Summary Cards

At the top of the Dashboard, you'll find five key performance indicators:

1. **Total Tickets**

    - Displays the total number of parking tickets created in the system
    - This includes both paid and unpaid tickets

2. **Paid Tickets**

    - Shows the number of tickets that have been successfully paid
    - Useful for tracking payment conversion rates

3. **Active Subscriptions**

    - Indicates the current number of active parking subscriptions
    - Only counts subscriptions that are valid for the current date

4. **Total Revenue**

    - Displays the cumulative revenue from all sources (tickets and subscriptions)
    - Shown in euros with two decimal places

5. **Today's Revenue**
    - Shows revenue generated only for the current day
    - Combines both ticket payments and subscription revenue for today

### Tickets Over Time Chart

This line chart visualizes ticket activity over the past 7 days:

-   **Created Tickets (Blue Line)**

    -   Represents the total number of tickets created each day
    -   Marked with square points for easy identification

-   **Paid Tickets (Orange Line)**

    -   Shows the number of tickets paid each day
    -   Marked with circular points

-   **Date Axis**
    -   The horizontal axis shows dates in MM/DD format
    -   Provides a week-long view of ticket trends

This chart helps identify patterns in parking usage and payment behavior over time.

### Ticket Status Distribution

This pie chart illustrates the proportion of tickets by payment status:

-   **Paid Tickets (Green)**

    -   Shows the percentage and count of tickets that have been paid

-   **Unpaid Tickets (Red)**
    -   Shows the percentage and count of tickets that remain unpaid

The chart automatically updates as ticket statuses change, providing a real-time view of payment completion rates.

### Revenue by Hospital Chart

This stacked column chart displays revenue generation across your top 5 performing hospitals:

-   **Ticket Revenue (Blue)**

    -   The lower portion of each column shows revenue generated from individual ticket payments

-   **Subscription Revenue (Green)**

    -   The upper portion shows revenue from parking subscriptions

-   **Hospital Labels**
    -   The horizontal axis shows hospital names
    -   Hospitals are sorted by total revenue in descending order

This visualization helps identify which facilities are generating the most revenue and through which channels.

### Payment Methods Distribution

This pie chart breaks down ticket payments by payment method:

-   Each slice represents a different payment method used (credit card, cash, etc.)
-   Subscription payments are consolidated into a single "Subscription" category
-   The chart legend shows the name of each payment method and its relative proportion

### Refreshing Dashboard Data

To update the dashboard with the latest information, click the "Refresh" button in the top-right corner of the dashboard.

**Note:** The dashboard automatically loads fresh data when you first navigate to it, but you should refresh manually if you keep the dashboard open for extended periods.

### Data Considerations

-   The dashboard shows data from the entire system history unless otherwise noted
-   "Today's Revenue" is calculated based on your local system time
-   Active subscriptions only count those valid on the current date
-   The top 5 hospitals by revenue are shown in the Revenue by Hospital chart; others are not displayed

## User Management

The User Management section allows administrators to view, create, edit, and manage user accounts in the BestTickets system. All users have administrative access to the system.

### Accessing User Management

Click on "User Management" in the side navigation drawer to access the interface with user accounts list and detailed account information.

### Viewing User Accounts

The User Management interface displays all user accounts in a list view:

-   Each entry shows the user's profile picture and full name
-   Disabled accounts appear with reduced opacity
-   By default, only active (non-disabled) accounts are shown

### Filtering User Accounts

1. **By Name**: Enter text in the filter box at the top-left of the interface

    - The list filters in real-time as you type
    - Filtering checks both first and last names

2. **Show Disabled Accounts**: Toggle the "Display Disabled" checkbox to show/hide inactive accounts

### Viewing User Details

Click on any user in the list to display their detailed information in the right panel.

### Editing User Information

1. Select the user you wish to edit from the list
2. Modify their information in the right panel
3. Click the "Save" button to apply changes

### Disabling/Enabling User Accounts

1. Select the user from the list
2. Check or uncheck the "Is Disabled" checkbox
3. Click "Save" to apply the change

### Deleting User Profile Pictures

1. Select the user from the list
2. Click the "Delete Image" button in the right panel
3. Confirm the deletion when prompted

### Creating New User Accounts

1. Click the "+" button at the bottom of the user list
2. A new user is created with default information
3. A dialog will appear showing a randomly generated temporary password
4. Copy the password to share with the new user
5. Edit the new user's information as needed
6. Click "Save" to apply your changes

**Important Notes about New Accounts:**

-   New users are created with active status by default
-   Account Creation Date is automatically set to the current date and time
-   Users should be instructed to change their temporary password upon first login
-   The temporary password is only shown once and cannot be retrieved later

### Security Best Practices

-   Create individual accounts for each administrator
-   Use unique, corporate email addresses for each account
-   Disable accounts promptly when no longer needed
-   Periodically review the list of active accounts
-   Remind users to change their temporary passwords immediately

## Profile Management

The Profile Management section allows you to view and modify your personal information, upload a profile picture, and change your password.

### Accessing Your Profile

Click on the Profile icon in the top navigation bar and select "View Profile" from the dropdown menu.

### Profile Information

Your profile page displays your account and contact information.

### Editing Your Profile

1. **Update Phone Number**: Edit the phone number field directly - changes save automatically when you click outside the field
2. **Upload Profile Picture**: Click "Upload Picture" to browse and select an image

### Changing Your Password

1. Enter your current and new passwords in the appropriate fields
2. Click the "Update Password" button

**Password Guidelines:**

-   Choose a strong, unique password with at least 8 characters
-   Include a mix of uppercase letters, lowercase letters, numbers, and symbols
-   Avoid using personal information or common words

**Note:** If you forget your password, contact your system administrator to reset it.

## Hospital Management

The Hospital Management section allows administrators to add, edit, and manage hospital facilities within the BestTickets system.

### Accessing Hospital Management

Click on "Hospital Management" in the side navigation drawer to access the hospitals interface.

### Viewing Hospitals

The Hospital Management page displays all registered hospitals in a card-based grid view:

-   Each hospital card shows the hospital's logo, name, and address
-   Cards are arranged in a grid that adjusts based on window size

### Managing Hospital Passwords

Each hospital has a secure access password that can be edited:

1. **Changing a Hospital Password**:

    - Locate the hospital card in the grid
    - Enter the new password in the password field
    - Click the "Edit Password" button
    - A confirmation message will appear when the password is updated

2. **Password Requirements**:
    - Choose a strong, unique password
    - Passwords are stored securely using cryptographic hashing
    - Hospital passwords are used for their access to the ticket system

### Deleting Hospitals

To remove a hospital from the system:

1. Find the hospital card in the grid view
2. Click the "Delete Hospital" button
3. Confirm the deletion in the prompt that appears
4. The hospital will be permanently removed from the system

**Warning**: Deleting a hospital removes all associated data and cannot be undone. Ensure all necessary data is backed up before deletion.

### Adding a New Hospital

To add a new hospital to the system:

1. **Access the Add Hospital Form**:

    - Click the "Show Add New Hospital" button at the bottom of the page
    - The add hospital form will expand

2. **Enter Hospital Details**:

    - **Name**: Enter the hospital's full name
    - **Address**: Enter the complete street address
    - **Password**: Create a secure password for hospital access
    - **Logo**: Upload the hospital's logo image (optional)

3. **Logo Requirements**:

    - Supported formats: PNG, JPEG, JPG
    - Images will be automatically resized to 200x200 pixels
    - For best results, use square images with a clear background

4. **Complete the Process**:
    - Click the "Add Hospital" button to create the new hospital
    - All fields except Logo are required
    - The new hospital will appear in the grid immediately after creation

### Best Practices

-   Maintain accurate and up-to-date information for each hospital
-   Use descriptive names that clearly identify each facility
-   Periodically review and update hospital passwords for security
-   Ensure each hospital has a distinctive logo for easy identification
-   Consider using a naming convention for hospitals with multiple locations

## Ticket Management

The Ticket Management section allows administrators to view, search, filter, and manage parking tickets throughout the system.

### Accessing Ticket Management

Click on "Ticket Management" in the side navigation drawer to access the interface showing the list of parking tickets and their details.

### Viewing Tickets

The Ticket Management interface displays parking tickets in a paginated table view:

-   Each row represents a single parking ticket
-   The table shows key information such as ticket number, creation time, hospital, and payment status
-   By default, tickets are sorted by creation time with the most recent tickets first

### Navigating Ticket Pages

The system displays tickets in pages to manage large datasets efficiently:

1. **Page Controls**: Located at the bottom of the ticket list

    - First Page: Jump to the first page of results
    - Previous Page: Move to the previous page
    - Page Indicator: Shows current page and total pages (e.g., "Page 2 of 10")
    - Next Page: Move to the next page
    - Last Page: Jump to the last page of results

2. **Items Per Page**: You can adjust how many tickets appear on each page

### Searching and Filtering Tickets

The Ticket Management section offers several filtering options to locate specific tickets:

1. **Text Search**:

    - Enter text in the search box to filter by ticket number or hospital name
    - Searches are case-insensitive and will match partial text

2. **Date Range Filtering**:

    - Start Date: Filter to show only tickets created on or after this date
    - End Date: Filter to show only tickets created on or before this date
    - When both dates are specified, the system will show tickets created within that range

3. **Payment Status Filtering**:

    - Use the dropdown to filter by payment status
    - Options include "All", "Paid", and "Unpaid"

4. **Applying Filters**:

    - Click the "Apply Filters" button after setting your filter criteria
    - The list will refresh to show only matching tickets

5. **Clearing Filters**:
    - Click "Clear Filters" to reset all filter criteria and show all tickets

### Sorting Tickets

You can sort the ticket list by any column:

1. Click on a column header to sort by that column
2. Click again to toggle between ascending and descending order
3. The current sort column is indicated by an arrow in the column header

Sortable columns include:

-   Ticket Number
-   Hospital Name
-   Creation Time
-   Payment Time
-   Departure Time

### Viewing Ticket Payment Details

For tickets that have been paid:

1. Click the "View Payment" button in the Actions column
2. A popup window will display detailed payment information:
    - Total payment amount
    - Tax amount
    - Amount before tax
    - Payment method
    - Discount code (if applied)
    - Subscription ID (if applicable)

### Managing Ticket Status

Administrators can manually change a ticket's payment status:

1. **Setting a Ticket as Paid**:

    - Locate the unpaid ticket in the list
    - Click the "Mark as Paid" button in the Actions column
    - The system will record payment with the current date/time
    - Payment method will be set as "admin-validated"

2. **Setting a Ticket as Unpaid**:
    - Locate the paid ticket in the list
    - Click the "Mark as Unpaid" button in the Actions column
    - The system will remove all payment information
    - Use this feature with caution, as it will delete payment records

### Exporting Ticket Information

Tickets can be exported in multiple formats:

1. **Export as PDF**:

    - Click the "PDF" button in the Actions column
    - Choose a save location in the file dialog
    - The system will generate a PDF document of the ticket
    - Option to open the PDF immediately after saving

2. **Export as PNG**:
    - Click the "PNG" button in the Actions column
    - Choose a save location in the file dialog
    - The system will generate a PNG image of the ticket
    - Option to open the image immediately after saving

### Best Practices

-   Regularly review unpaid tickets that are past their expected payment time
-   Use the date filter to identify older tickets that may need attention
-   Export tickets as PDF for record-keeping or when sharing information with other departments
-   Use the admin payment validation feature only in exceptional circumstances
-   Document the reason for manual status changes in your internal records

### Data Considerations

-   Ticket data is pulled from the live database and reflects the current state of the system
-   Payment details are only available for tickets marked as paid
-   Large ticket datasets may take a moment to load, especially when applying complex filters
-   Exported files (PDF/PNG) are not automatically stored in the system and should be saved to a secure location

## Subscription Management

The Subscription Management section allows administrators to view and manage parking subscriptions across all hospitals in the system.

### Accessing Subscription Management

Click on "Subscription Management" in the side navigation drawer to access the interface showing all active and expired subscriptions.

### Viewing Subscriptions

The Subscription Management interface displays all subscriptions in a table format with the following information:

-   **Card Number**: The unique identifier for the subscription card
-   **Hospital**: The hospital where the subscription is valid
-   **Start Date**: When the subscription period begins
-   **End Date**: When the subscription period expires
-   **Maximum Uses Per Day**: The number of times the subscription can be used daily
-   **Payments Count**: The number of parking tickets paid using this subscription
-   **Subscription Status**: Whether the subscription is active or expired based on current date

### Understanding Subscription Status

Subscriptions are automatically marked as:

-   **Active**: When the current date falls between the start and end dates
-   **Expired**: When the current date is after the end date
-   **Upcoming**: When the current date is before the start date

### Exporting Subscription Cards

You can export subscription cards in two formats for printing or digital distribution:

1. **Export as PDF**:

    - Locate the subscription in the table
    - Click the PDF icon button in the Actions column
    - Choose a save location in the file dialog
    - The system will generate a PDF document of the subscription card
    - Option to open the PDF immediately after saving

2. **Export as PNG**:
    - Locate the subscription in the table
    - Click the image icon button in the Actions column
    - Choose a save location in the file dialog
    - The system will generate a PNG image of the subscription card
    - Option to open the image immediately after saving

### Best Practices

-   Regularly audit subscription usage to identify potential misuse patterns
-   Export subscription cards as PDF when you need to email them to customers
-   Use PNG format when you need to include the card in other documents or systems
-   Review subscriptions nearing expiration to notify customers about renewal
-   Monitor the payments count to identify subscriptions with unusual activity

### Data Considerations

-   Subscription data is pulled directly from the live database
-   The subscription list includes both active and expired subscriptions
-   Subscription status is calculated in real-time based on the current date
-   The maximum uses per day setting cannot be modified after subscription creation
-   Exported files (PDF/PNG) are not automatically stored in the system and should be saved to a secure location

### Active Subscriptions

To view only currently active subscriptions:

1. Note the "Subscription Status" column in the table
2. Look for subscriptions marked as "Active"
3. These subscriptions are valid for the current date

Active subscriptions contribute to the "Active Subscriptions" count displayed on the dashboard.

### Subscription Tiers

Subscription tiers are predefined offerings with different durations and usage limits:

1. **Managing Subscription Tiers**:

    - Tiers are configured at the hospital level or set as global (applicable to all hospitals)
    - Each tier has a specific price point, duration, and usage allowance
    - Administrators can add, delete, and copy existing subscription tiers
    - Global tiers apply only to hospitals that don't have their own specific tiers
    - When you create a hospital-specific tier, that hospital will no longer use global tiers

2. **Subscription Properties**:

    - **Name**: Descriptive title for the subscription tier (e.g., "Monthly Standard", "Annual Premium")
    - **Duration**: How long the subscription remains valid (days, months, or years)
    - **Maximum Uses Per Day**: How many times a subscription can be used within a 24-hour period
    - **Price**: The cost of the subscription in local currency
    - **Hospital**: The specific hospital this tier applies to, or "Global" for system-wide tiers

3. **Adding New Subscription Tiers**:

    - Navigate to the Subscription Tiers management page
    - Fill in the required fields at the bottom of the page:
        - Name: Enter a descriptive name for the tier
        - Duration in Days: Enter the number of days the subscription will be valid
        - Max Uses Per Day: Set the daily usage limit
        - Price: Set the cost for this subscription tier
        - Hospital: Select a specific hospital or "Global" from the dropdown
    - Click "Add Subscription Tier" to create the new tier

4. **Managing Existing Tiers**:

    - **Delete**: Remove a tier by clicking the delete icon (only available if no active subscriptions use this tier)
    - **Copy**: Duplicate an existing tier's settings by clicking the copy icon, then modify as needed
    - All subscription tiers are displayed in a table showing name, duration, max uses, price, and associated hospital

5. **Subscription Card Information**:
    - Each subscription card displays:
        - Card number for identification
        - Customer's vehicle plate number
        - Valid dates (start and end)
        - Hospital name and logo
        - Daily usage limit
        - Scannable QR code for validation

### Refreshing Subscription Data

To ensure you're viewing the most current subscription information:

1. Navigate away from the Subscription Management page and return
2. This will automatically reload all subscription data from the database
3. Alternatively, use the refresh button in the top-right corner of the subscription list, if available

## Discount Codes

The Discount Codes section allows administrators to create and manage promotional discounts for parking services across all hospitals.

### Accessing Discount Codes Management

Click on "Discount Codes" in the side navigation drawer to access the interface showing all discount codes in the system.

### Viewing Discount Codes

The Discount Codes interface displays all codes in a table format with the following information:

-   **Name**: Short identifier for the discount code
-   **Description**: Detailed explanation of the discount code's purpose or rules
-   **Hospital**: Which hospital the code applies to (or "Global" if applicable to all)
-   **Reduction**: Percentage discount applied when the code is used
-   **Active**: Whether the code is currently usable
-   **Actions**: Buttons to enable/disable, delete, or copy the code

### Managing Discount Codes

#### Filtering Discount Codes

By default, only active discount codes are displayed in the table. To view inactive codes:

1. Check the "Show Disabled Codes" checkbox in the top-right corner
2. The table will update to include both active and inactive discount codes

#### Enabling or Disabling Codes

To temporarily deactivate a code without deleting it:

1. Locate the discount code in the table
2. Click the "Disable" button in the Actions column
3. The code will be marked as inactive and won't be usable until re-enabled
4. To re-enable, click the "Enable" button that appears for inactive codes

#### Deleting Discount Codes

To permanently remove a discount code:

1. Locate the discount code in the table
2. Click the delete icon (trash can) in the Actions column
3. The code will be permanently removed from the system
4. Note: This action cannot be undone

#### Copying Discount Codes

To create a new code based on an existing one:

1. Locate the discount code you want to use as a template
2. Click the copy icon in the Actions column
3. The new code form will be populated with the selected code's information
4. Modify any fields as needed
5. Click "Add Discount Code" to create the new code

### Creating New Discount Codes

To create a new discount code:

1. Scroll to the "Add New Discount Code" section at the bottom of the page
2. Fill in the required fields:
    - **Name**: Enter a short, descriptive name for the code
    - **Description**: Provide details about the code's purpose or usage instructions
    - **Reduction**: Enter the percentage discount (1-100) to be applied
    - **Active**: Check this box to make the code immediately available for use
    - **Hospital**: Select a specific hospital from the dropdown, or choose "Global" to apply to all hospitals
3. Click "Add Discount Code" to create the new code
4. The new code will appear in the discount codes table

### Best Practices

-   Use clear, descriptive names that indicate the purpose of the discount
-   Include expiration dates or usage conditions in the description field
-   Deactivate codes instead of deleting them to maintain historical records
-   Limit high-value discount codes to specific hospitals rather than making them global
-   Regularly audit active discount codes to ensure they remain relevant

### Data Considerations

-   Discount codes apply a percentage reduction to the parking fee
-   The reduction value must be between 1 and 100 percent
-   Global discount codes apply to all hospitals in the system
-   Hospital-specific codes only apply to the selected hospital
-   Active/inactive status can be toggled at any time

## Price Brackets

The Price Brackets section allows administrators to configure time-based pricing structures for parking across all hospitals in the system.

### Accessing Price Brackets Management

Click on "Price Brackets" in the side navigation drawer to access the interface showing all pricing tiers in the system.

### Viewing Price Brackets

The Price Brackets interface displays all pricing tiers in a table format with the following information:

-   **Minimum Duration**: The minimum time a vehicle must be parked to qualify for this price bracket
-   **Price**: The cost that will be charged when this bracket applies
-   **Hospital**: Which hospital the price bracket applies to (or "Global" if applicable to all)
-   **Actions**: Buttons to delete or copy the price bracket

### Understanding Price Brackets

Price brackets define how much customers pay based on how long they park:

-   The system automatically selects the appropriate price bracket based on parking duration
-   Brackets are defined with a minimum duration threshold (e.g., 30 minutes, 1 hour, 2 hours)
-   When a vehicle exits, the system calculates the total parking time and applies the appropriate bracket
-   If multiple brackets could apply, the system selects the one with the longest duration that doesn't exceed the actual parking time

### Managing Price Brackets

#### Deleting Price Brackets

To permanently remove a price bracket:

1. Locate the price bracket in the table
2. Click the delete icon (trash can) in the Actions column
3. The price bracket will be permanently removed from the system
4. Note: This action cannot be undone

#### Copying Price Brackets

To create a new price bracket based on an existing one:

1. Locate the price bracket you want to use as a template
2. Click the copy icon in the Actions column
3. The new price bracket form will be populated with the selected bracket's information
4. Modify any fields as needed
5. Click "Add Price Bracket" to create the new price bracket

### Creating New Price Brackets

To create a new price bracket:

1. Navigate to the bottom section of the Price Brackets page
2. Fill in the required fields:
    - **Minimum Duration**: Enter the time threshold in the format "hh:mm:ss" (e.g., "01:30:00" for 1 hour and 30 minutes)
    - **Price**: Enter the amount to charge when this bracket applies
    - **Hospital**: Select a specific hospital from the dropdown, or choose "Global" to apply to all hospitals
3. Click "Add Price Bracket" to create the new price bracket
4. The new bracket will appear in the price brackets table

### Global vs. Hospital-Specific Price Brackets

The system supports two types of price brackets:

1. **Global Price Brackets**:

    - Apply to all hospitals that don't have their own specific price brackets
    - Created by selecting "Global" in the Hospital dropdown
    - Serve as a default pricing structure for the entire system

2. **Hospital-Specific Price Brackets**:
    - Apply only to a selected hospital
    - Override any global price brackets for that hospital
    - Created by selecting a specific hospital in the Hospital dropdown
    - Allow for customized pricing at individual locations

### Best Practices

-   Create a comprehensive set of brackets covering various durations (e.g., 15 min, 30 min, 1 hour, 2 hours, etc.)
-   Use consistent time intervals for easier customer understanding
-   Consider peak/off-peak pricing by creating separate brackets for different hospitals
-   Review pricing structures regularly based on usage patterns
-   Ensure there are no significant gaps in your duration coverage

### Data Considerations

-   Duration must be entered in the "hh:mm:ss" format (hours:minutes:seconds)
-   Price values should be entered without currency symbols
-   When a hospital has specific price brackets, global brackets won't apply to that hospital
-   The system automatically sorts and applies price brackets based on duration
-   There is no limit to how many price brackets you can create per hospital

## Gate Control

The Gate Control section allows administrators to manually open parking entry and exit gates for specified durations.

### Accessing Gate Control Management

Click on "Gate Control" in the side navigation drawer to access the interface for controlling parking gates remotely.

### Understanding Gate Status

The Gate Control interface displays the current status of both entry and exit gates:

-   **Entry Gate**: Shows whether the gate is currently open or closed
-   **Exit Gate**: Shows whether the gate is currently open or closed

When a gate is open, the remaining time until automatic closure is displayed next to the status.

### Opening Gates

To open a parking gate:

1. **Select a Hospital**:

    - Choose the hospital where you want to control gates from the dropdown menu
    - The system will immediately show the current status of gates at that location

2. **Select Gate Type**:

    - Choose whether to open the Entry Gate or Exit Gate using the radio buttons
    - Entry gates control vehicle access into the parking area
    - Exit gates control vehicle departure from the parking area

3. **Set Open Duration**:

    - Specify how long the gate should remain open using the hours, minutes, and seconds controls
    - The default duration is 1 minute
    - Maximum duration is 23 hours, 59 minutes, 59 seconds
    - Minimum duration is 1 second

4. **Send Open Signal**:
    - Click the "Open Gate" button to send the signal
    - A confirmation message will appear indicating the signal was sent successfully
    - The gate status display will update to show the gate is now open and the remaining time

### Canceling Gate Signals

To close a gate before its scheduled duration expires:

1. Locate the active gate signal in the status display at the top of the interface
2. Click the "X" button next to the remaining time display
3. The gate will immediately close and the status will update to "No active signal"

### Active Signals

The system tracks active gate signals with the following information:

-   Which gate is currently open (entry or exit)
-   How much time remains before automatic closure
-   Options to cancel the signal early

If you send a new signal while a gate is already open, the system will update the existing signal and extend the open duration based on your new settings.

### Security Considerations

-   Opening gates should be done only when necessary for operational requirements
-   All gate operations are logged in the system with the administrator's username
-   Gates automatically close after the specified duration to prevent security issues
-   Only authorized personnel with appropriate permissions can access the Gate Control feature
-   Remote gate operation should comply with facility security protocols

### Best Practices

-   Use short durations (1-5 minutes) for routine operations
-   Monitor gate status after sending open signals
-   Cancel signals immediately if they are no longer needed
-   Coordinate with on-site personnel when operating gates remotely
-   Use longer durations only for special circumstances (maintenance, special events)
-   Review gate operation logs periodically to ensure proper usage

### Technical Information

-   Gate signals are sent securely to the gate control hardware
-   The system continuously checks for active signals
-   If the connection to the gate hardware is lost, gates will default to their closed state
-   Gates may also be operated locally using physical controls at each location
-   Active signals are stored in the database and persist even if the application is closed

## Tax Management

The Tax Management section allows administrators to create and manage tax rates that apply to parking fees across the system.

### Accessing Tax Management

Click on "Tax Management" in the side navigation drawer to access the interface for configuring and managing tax rates.

### Viewing Tax Rates

The Tax Management interface displays all configured tax rates in a table format with the following information:

-   **Name**: The identifier for the tax (e.g., "VAT", "Sales Tax", "City Tax")
-   **Amount**: The percentage rate applied (displayed as a percentage)
-   **Description**: Optional details about the tax's purpose or applicability
-   **Actions**: Buttons to delete or copy the tax configuration

### Managing Tax Rates

#### Deleting Tax Rates

To permanently remove a tax rate from the system:

1. Locate the tax rate in the table
2. Click the delete icon (trash can) in the Actions column
3. The tax rate will be permanently removed from the system
4. Note: This action cannot be undone and may affect billing calculations

#### Copying Tax Rates

To create a new tax rate based on an existing one:

1. Locate the tax rate you want to use as a template
2. Click the copy icon in the Actions column
3. The "Add New Tax" form will be populated with the selected tax's information
4. Modify any fields as needed
5. Click "Add Tax" to create the new tax rate

### Creating New Tax Rates

To add a new tax rate to the system:

1. Navigate to the "Add New Tax" section at the bottom of the page
2. Fill in the required fields:
    - **Name**: Enter a descriptive name for the tax (e.g., "VAT", "Sales Tax")
    - **Amount**: Enter the tax rate as a decimal value (e.g., 0.15 for 15%)
    - **Description**: (Optional) Provide additional information about when or how this tax applies
3. Click "Add Tax" to create the new tax rate
4. The new tax will appear in the tax rates table

### Tax Application

The configured tax rates affect how parking fees are calculated throughout the system:

-   Tax rates are applied automatically to all parking transactions
-   The system calculates the tax amount based on the configured percentage
-   Tax amounts are displayed separately on receipts and financial reports
-   Multiple taxes can be configured and will be applied cumulatively

### Best Practices

-   Use clear, descriptive names for tax rates for easy identification
-   Include regulatory information in the description field when applicable
-   Review tax configurations regularly to ensure compliance with local regulations
-   Update tax rates promptly when tax laws or percentages change
-   Consider using the copy feature when creating similar tax rates (e.g., different regions)

### Data Considerations

-   Tax amounts must be entered as decimal values (e.g., 0.15 for 15%)
-   Tax descriptions are optional but recommended for documentation purposes
-   All tax rate changes take effect immediately system-wide
-   Historic transactions retain the tax rates that were active at the time of the transaction
-   The system does not support tax exemptions or conditional tax application

## Settings

The Settings section allows administrators to customize the application appearance and behavior according to their preferences.

### Accessing Settings

Click on "Settings" in the side navigation drawer to access the interface for configuring application preferences.

### Appearance Settings

#### Language Selection

You can change the application's language:

1. Select your preferred language from the dropdown menu
2. Options include:
    - English (en)
    - French (fr)
3. The interface will immediately update to reflect your selected language
4. All text elements, including navigation items, will be translated

#### Theme Selection

You can toggle between light and dark themes:

1. Select your preferred theme from the dropdown menu
2. Options include:
    - Light: A bright, white-based interface
    - Dark: A dark interface that reduces eye strain in low-light environments
3. The application will immediately apply the selected theme
4. All windows and dialogs will automatically use the selected theme

#### Primary Color

You can personalize the application's color scheme:

1. Select your preferred primary color from the dropdown menu
2. Options include:
    - Blue (default)
    - Deep Purple
    - Indigo
    - Teal
    - Green
    - Orange
    - Red
    - Pink
3. The application will immediately update all UI elements with the selected color
4. The secondary accent color (lime) remains consistent across all primary color choices

### Settings Persistence

-   All settings are automatically saved when changed
-   Settings persist between application sessions
-   Settings are stored locally in a settings.json file
-   No server connection is required to save or load settings

### Best Practices

-   Choose a theme that provides the best visibility in your work environment
-   Consider using dark theme for extended usage periods to reduce eye strain
-   Select a primary color that offers good contrast with the interface text
-   Change the language setting to match the primary language of the users

### Technical Information

-   Settings are applied instantly without requiring application restart
-   The application automatically detects your system language on first launch
-   Settings are stored in the application's local directory
-   All preferences can be modified at any time without affecting system functionality or data

## Troubleshooting

### Common Issues and Solutions

#### Application Won't Start

-   **Issue**: The Admin Software fails to launch or crashes immediately.
-   **Solution**:
    1. Verify .NET 6.0 or later is installed on your system
    2. Check that the appsettings.json file exists and is not corrupted
    3. Ensure you have proper permissions to the application directory
    4. Run the application as administrator

#### Database Connection Failures

-   **Issue**: "Unable to connect to database" error appears.
-   **Solution**:
    1. Verify MySQL server is running
    2. Check connection string in appsettings.json
    3. Ensure firewall is not blocking database connections
    4. Verify network connectivity between application and database server

#### Login Problems

-   **Issue**: Unable to log in despite correct credentials.
-   **Solution**:
    1. Verify account is not disabled
    2. Check caps lock is not enabled
    3. Ensure network connectivity to authentication server
    4. Clear browser cache if using web interface
    5. Contact administrator to reset password if necessary

#### Missing Menu Items

-   **Issue**: Some menu options are not visible in the navigation drawer.
-   **Solution**:
    1. Verify your user account has appropriate permissions
    2. Log out and log back in to refresh permissions
    3. Check with administrator to ensure role assignments are correct

#### Gate Control Not Working

-   **Issue**: Gates don't respond to open commands.
-   **Solution**:
    1. Verify gate hardware is powered on and operational
    2. Check network connectivity to gate controllers
    3. Ensure hospital is correctly selected in the Gate Control interface
    4. Verify server API is running and responding
    5. Check physical gate status at the location

#### Tax Calculations Incorrect

-   **Issue**: Tax amounts don't match expected values.
-   **Solution**:
    1. Verify tax rates are correctly configured in Tax Management
    2. Check that price brackets are properly set up
    3. Ensure the correct hospital is associated with the ticket
    4. Review any recent tax rate changes that might affect calculations

#### Settings Not Saving

-   **Issue**: Application settings revert after restart.
-   **Solution**:
    1. Verify write permissions to the application directory
    2. Check if settings.json file is read-only
    3. Ensure you're not using a guest account
    4. Try running the application as administrator

#### Subscription Card Export Fails

-   **Issue**: Unable to export subscription cards as PDF or PNG.
-   **Solution**:
    1. Verify write permissions to the destination folder
    2. Ensure QuestPDF and related dependencies are properly installed
    3. Check if the subscription data is complete and valid
    4. Try restarting the application

#### Price Brackets Not Applied Correctly

-   **Issue**: System charges incorrect amounts for parking.
-   **Solution**:
    1. Check price bracket configurations for overlaps or gaps
    2. Ensure hospital-specific brackets are set up if needed
    3. Verify server time synchronization is accurate
    4. Review tickets to ensure parking duration is calculated correctly

#### Dashboard Charts Not Loading

-   **Issue**: Dashboard displays empty or incomplete charts.
-   **Solution**:
    1. Check database connection
    2. Verify reporting data is available for the selected time period
    3. Try refreshing the dashboard using the refresh button
    4. Restart the application if necessary

### Getting Additional Help

If you continue to experience issues after trying these troubleshooting steps:

1. **Contact Support**:
    - Email: jeremy.guerin34@yahoo.com

When contacting support, please provide:

-   Your software version (shown on the About screen)
-   Detailed description of the issue
-   Steps to reproduce the problem
-   Any error messages displayed
-   Screenshots if applicable

## Frequently Asked Questions

### General Application Questions

#### Q: What is BestTickets Admin Software?

A: BestTickets Admin Software is a comprehensive management system for hospital parking facilities that handles ticket processing, subscription management, gate control, and financial reporting. It serves as the administrative control center for the entire parking system.

#### Q: How often is the software updated?

A: Never

#### Q: Can multiple administrators use the system simultaneously?

A: Yes, the system supports concurrent users. Each administrator uses their own account with specific permissions, and all actions are tracked in the audit log.

#### Q: Is there a mobile version of the admin software?

A: No

### Technical Questions

#### Q: What are the minimum system requirements?

A: The minimum requirements are Windows 10 (64-bit), 4GB RAM, 500MB free disk space, 1366x768 screen resolution, and .NET 6.0 runtime.

#### Q: Does the system work offline?

A: No, the admin software requires a connection to the database server. Some features may cache data temporarily, but core functions require network connectivity.

#### Q: How do I update to the latest version?

A: Updates are automatically detected and installed when you launch the application. Manual updates can be performed through the Help menu by selecting "Check for Updates."

#### Q: Can I run the software on Mac or Linux?

A: While the core application is built on .NET, it's officially supported only on Windows. Running on Mac or Linux may be possible using compatibility layers but is not officially supported.

### User Management Questions

#### Q: Can I recover a forgotten password?

A: No, there is no self-service password recovery. Contact your system administrator to have your password reset.

#### Q: Why can't I delete a user account?

A: For audit purposes, user accounts cannot be permanently deleted. Instead, accounts should be disabled when no longer needed.

#### Q: What happens to tickets processed by a disabled user?

A: Tickets and other data created by a user remain in the system even after the user account is disabled. The audit history maintains the association with the original user.

#### Q: How do I assign different permission levels to users?

A: Currently all admin users have full permissions. Role-based access is supported but not relevant to the current system.

### Ticket Management Questions

#### Q: Can I edit a ticket after it's been created?

A: No, ticket information cannot be edited for security and audit purposes. You can only change a ticket's payment status.

#### Q: How long are tickets stored in the system?

A: Tickets are stored indefinitely. There is no automatic purging of ticket data, though this may be implemented in future versions.

#### Q: What should I do if a customer claims to have paid but the ticket shows as unpaid?

A: Verify their payment receipt, check payment logs in the system, and if you can confirm payment occurred, use the "Mark as Paid" feature to update the status.

#### Q: Can I issue refunds through the admin software?

A: No, the system does not directly process refunds. Refunds must be handled through your financial department or payment processor.

### Subscription Management Questions

#### Q: How are subscription fees calculated?

A: Subscription fees are fixed amounts set in the Subscription Tiers section. They are not calculated based on usage.

#### Q: Can customers have multiple active subscriptions?

A: Yes, a customer can have multiple subscriptions, even for the same hospital.

#### Q: What happens when a subscription expires?

A: When a subscription expires, it automatically becomes inactive and can no longer be used for parking validation. The system retains the subscription record for historical purposes.

#### Q: Can I extend an existing subscription?

A: No, subscriptions cannot be extended after creation. Instead, create a new subscription for the customer when the current one expires.

### Price Bracket Questions

#### Q: Do price changes affect existing tickets?

A: No, tickets use the price brackets that were active at the time of their creation. Changing prices will only affect new tickets.

#### Q: Can I set different prices for weekdays and weekends?

A: Currently, the system doesn't support time-of-day or day-of-week pricing. Consider manually changing prices based on the day if needed.

#### Q: How do I implement a free parking period?

A: Create a price bracket with 0.00 as the price for the desired duration (e.g., first 15 minutes free).

### Gate Control Questions

#### Q: What happens if I close the admin software while a gate is open?

A: Gate signals are stored on the server and persist even if you close the admin software. Gates will close automatically when their designated open duration expires.

#### Q: Can I schedule gates to open automatically at certain times?

A: No, the current version does not support scheduled gate operations.

#### Q: Is there a limit to how many gates I can control?

A: You can control all gates across all hospitals that are configured in the system.

#### Q: Can I see a log of gate operations?

A: Yes, all gate operations are recorded in the system log with timestamps and the operator's username.

### Reporting and Dashboard Questions

#### Q: How current is the dashboard data?

A: Dashboard data is refreshed when you first load the dashboard. For real-time updates, use the refresh button in the top-right corner of the dashboard.

#### Q: Can I export dashboard data to Excel?

A: Currently, dashboard data cannot be directly exported.

#### Q: Why doesn't my hospital appear in the Revenue by Hospital chart?

A: The Revenue by Hospital chart shows only the top 5 hospitals by revenue. If your hospital isn't shown, it's not currently among the top 5 revenue generators.

#### Q: How can I see data for a specific time period?

A: Currently, the dashboard shows predefined time periods. Custom date range selection for dashboard metrics will be available in a future update.

### Financial Questions

#### Q: How are taxes calculated?

A: Taxes are calculated based on the percentages defined in the Tax Management section and are applied to the base ticket price.

#### Q: Can I issue a ticket without collecting payment?

A: Tickets are created when vehicles enter the parking area. Payment is only collected when the user pass through the payment terminal.

#### Q: Does the system support multiple currencies?

A: Currently, the system supports a single currency configuration (Cad by default).

#### Q: How can I reconcile daily payments?

A: Use the Ticket Management section with date filters to view all paid tickets for a specific day. This data can be used for reconciliation with your financial systems.


# Usage Guide for BestTickets Gate Software

## Introduction
BestTickets Gate Software is designed to manage vehicle entry and exit operations at parking facilities. The software supports both entry gates (ticket generation) and exit gates (ticket validation) with a user-friendly interface.
This is the usage guide for the Gate Software part of the BestTickets software suite.

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


# Payment Terminal User Guide

## Introduction
The Best Tickets Payment Terminal is an application for processing parking ticket payments at hospitals and creating subscription. This usage guide provides an overview of the payment terminal part of the Best Ticket software suite.

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