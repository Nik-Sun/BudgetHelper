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
<img src="https://github.com/user-attachments/assets/9804c3d2-3366-4257-b54e-bf98dc1c89c2" width=50%/>
![Screenshot_1728926127](https://github.com/user-attachments/assets/9804c3d2-3366-4257-b54e-bf98dc1c89c2)
