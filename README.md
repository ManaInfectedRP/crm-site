﻿# CRM-Project 

### CRM Länkar
- ( CRM_Förstudie ) https://docs.google.com/document/d/1-Oq8TGTh17ZhH_6MfP6KS1kZsMwbEXbKTGaiG7TXYxw/edit?usp=sharing
- ( Testfall_CRMSupport ) https://docs.google.com/spreadsheets/d/1mSlT9mL-76r3pYJGQzDBfUYr_Vr7AwJGe6qO_KWrqdk/edit?usp=sharing
- ( CRM_Slutrapport ) https://docs.google.com/document/d/14wNas9D50RstrI10c1s1aVnEtjHoqVsTo4x_QvjHKJU/edit?usp=sharing

- ( Testning - Kunskapsfårgor ) : https://docs.google.com/document/d/1Yf0VQTZpnJcjd-MrZ8-BDW7hgsTpjM7S2gnHTd8lVoU/edit?usp=sharing

## Setup
### Install dependencis
- In the terminal, open client and run "npm install". 
- In the terminal, open server and run "dotnet restore".

### Database

- Create an PostgreSQL database, open a query console, copy the script from "Database-Script.sql" and run it.
- Go in to the Database.cs and add your PostgreSQL connection information. 

Example
```c#
private readonly string _host = "localhost";
private readonly string _port = "5432";
private readonly string _username = "postgres";
private readonly string _password = "password";
private readonly string _database = "crm-site";
```

### Email

- In server, open the appsettings.json and configure the "Email"-object. You need to sett the email and password for the "Email".  

Example
```txt
"Email": {
    "SmtpServer": "smtp.gmail.com", // If your not using a gmail, you need to configure this.
    "SmtpPort": 587, 
    "FromEmail": "your-email",
    "Password": "your-password"
  }
```

