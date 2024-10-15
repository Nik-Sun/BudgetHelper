# Home Budget App
## Overview

Home Budget App is a simple mobile application built with .NET MAUI, designed to help users manage their expenses efficiently. The app allows users to categorize expenses, track their spending habits, and view financial reports by month and category.

**Note:** The app is currently available only in <mark>Bulgarian</mark>. 

## Features

- Expense Tracking: Add, view, and delete expenses for different categories and months.
- Category Management: Organize expenses into categories like groceries, communal, fun, and more.
- Expense Types: Assign types to expenses for detailed tracking (e.g., electricity under communal).
- Monthly View: View all expenses for a specific month and category.
- Comprehensive View: Display all expenses for a given type, offering a flexible way to filter and analyze data.
- Advice Feature: Get random life advice from [AdviceSlip API](https://api.adviceslip.com/).

## Tech Stack

- Framework: [.NET MAUI](https://dotnet.microsoft.com/en-us/apps/maui)
- Backend: [SQLite](https://www.sqlite.org/) with EF Core for local data storage
- UI: [DevExpress](https://www.devexpress.com/) for chart control

## Database Schema

- Categories Table: Stores different categories of expenses like Communal, Groceries, Fun
- ExpenseTypes Table: Stores some predefined expense types like Electricity, Phones etc. Additional ExpenseTypes can be defined later in the app.Each expense type has a Category.
- Expenses Table: Each expense is linked to a ExpenseType, with additional fields for amount and date.


## Screenshots
### App Title Screen
<img src="https://github.com/user-attachments/assets/e268c9a5-6a83-49b4-a986-fc2776ee9f47" width=30%/>


### Adding Expense Screen
<img src="https://github.com/user-attachments/assets/7ecb23ea-f4d0-48ec-993a-2cee1af8c595" width=30%/>



### Expense Tracking Screen
<img src="https://github.com/user-attachments/assets/3b5e7b35-7438-4063-a201-94a4b571b4f9" width=30%/>
<img src="https://github.com/user-attachments/assets/55cb0a5b-88f5-45bb-8766-63831a4f0936" width=30%/>


### Expenses By Category Screen
<img src="https://github.com/user-attachments/assets/709d6883-cb62-426a-ac4d-92944530cc79" width=30%/>

## Future Improvements
 - Multi-language support (Currently only in Bulgarian)
 - Nicer Navigation transitions

## Try the App

You can download the latest version of the Home Budget App for testing purposes:

- [Download Home Budget App (APK)](https://www.dropbox.com/scl/fi/1ulcc5oiypqb5pyp40qh1/com.companyname.budgethelper-arm64-v8a-Signed.apk?rlkey=w4imv1b1z7ml8t92g4ql4dmor&dl=0)

### Important Notes:
- This app uses a **self-signed certificate**, which means you may receive security warnings during installation. Please only install if you're comfortable with that.
- The app is currently in a **beta stage** and is not intended for professional or production use.
  
### Installation Instructions for Android:
1. Download the APK from the link above.
2. On your device, go to **Settings** > **Security** > **Unknown Sources** and enable it to allow installations from sources other than the Play Store.
3. Open the APK file and follow the prompts to install the app.
4. After installation, you can disable "Unknown Sources" for added security.
