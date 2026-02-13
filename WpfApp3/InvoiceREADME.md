# 📄 Invoice Project -- Setup Guide

This guide explains how to properly set up and run the Invoice Project
on your local machine.

------------------------------------------------------------------------

## 🛠 Prerequisites

Before starting, make sure you have:

-   SQL Server installed (SQL Server / SQL Server Express)
-   SQL Server Management Studio (SSMS)
-   Visual Studio (2019 / 2022 recommended)
-   .NET SDK installed

------------------------------------------------------------------------

## 🗄 Step 1: Create the Database

1.  Open **SQL Server Management Studio (SSMS)**.
2.  Connect to your SQL Server instance.
3.  Create a new database:
    -   Right-click **Databases**
    -   Select **New Database**
    -   Enter the database name (e.g., `InvoiceDB`)
    -   Click **OK**
4.  Open the provided database script file.
5.  Copy the full SQL script.
6.  Paste it into a new query window in SSMS.
7.  Make sure the correct database is selected.
8.  Click **Execute** to run the script.

⚠ Important:\
Run the SQL script sequentially without skipping any section to ensure
all tables, relationships, and data are created correctly.

------------------------------------------------------------------------

## 💻 Step 2: Configure the Project

1.  Open the **Invoice Project** in Visual Studio.

2.  Locate the `appsettings.json` file (for .NET Core projects)\
    OR\
    `Web.config` (for older .NET Framework projects).

3.  Update the **Connection String** with your SQL Server details.

Example:

``` json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=InvoiceDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Replace: - `YOUR_SERVER_NAME` with your SQL Server instance name. -
`InvoiceDB` with your actual database name (if different).

------------------------------------------------------------------------

## ▶ Step 3: Run the Project

1.  Build the solution:
    -   Build → Build Solution
2.  Run the project:
    -   Press F5\
        OR\
    -   Click Start.
3.  The application should now launch successfully.

------------------------------------------------------------------------

## ✅ Troubleshooting

-   If you get a database connection error, double-check:
    -   SQL Server is running.
    -   The connection string is correct.
    -   The database exists.
-   If tables are missing:
    -   Re-run the SQL script.
    -   Ensure it executed without errors.

------------------------------------------------------------------------

## 🎯 Project Ready

Once the database is connected and the project runs successfully, the
Invoice system is ready for use.
