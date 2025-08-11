# PetManagerRazor

**PetManagerRazor** is an ASP.NET Core Razor Pages web application to manage pet owners and their pets. It allows users to create, view, edit, and delete owners and pets, with features like search, validation, and relational data handling.

## Features

- Manage pet owners (add, edit, delete)
- Manage pets linked to owners
- Search functionality for owners and pets by name
- Validation on required fields and data types
- Clean UI with Bootstrap for responsive design
- Data persistence with Entity Framework Core and SQL Server

## Technologies Used

- ASP.NET Core Razor Pages
- Entity Framework Core
- SQL Server (or your preferred database)
- Bootstrap 5
- C#

## Setup Notes

- Configure your database connection string in `appsettings.json`
- Run EF Core migrations to create/update the database

 ## Future Improvements

- Add a Visit model to track pet appointments
- Extend Pet model with additional details:
  - Weight
  - Vaccination records
  - Gender
  - Fixed status (spayed/neutered)
- User authentication and authorization
- UI/UX enhancements
