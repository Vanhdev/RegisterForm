# Register form

## Installation

Install MySql and MySql connector 8.0.3

Create database MySQL

```
create database [database_name];
```

Change connection string in appsettings.Developmemt.json and appsettings.json

```
"ConnectionStrings": {
    "DefaultConnection": "server=[host_name]; port=[port]; database=[database_name]; user=[user_name]; password=[password]; Persist Security Info=False; Connect Timeout=300"
}
```

Run command
```bash
dotnet ef database update
```

## Introduction

Click "login" to redirect to Login page.

Fill in the form to login or click "You do not have a account!" to redirect to Register page.

Fill in the form to register then login.

