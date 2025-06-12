# 📚 Student Registration System

This repository contains a **student registration system** project developed in **C#**. The system allows for student registration, local database storage, and automatic address retrieval via a ZIP code API.

## 📝 Project Description

The goal of this project is to create a simple system for registering students or users. Users can add new students, save their information, and use an external API to automatically fetch address details based on the entered ZIP code. All student data is stored in a **SQL Server** database to ensure persistence.

## 🚀 Features

- **Student Registration**: Register students with name, email, phone number, birth date, address, and other relevant details.
- **Address Lookup via ZIP Code API**: When a ZIP code is entered, the system fetches the address fields (street, neighborhood, city, state) automatically using an external API.
- **Database Storage**: Student data is saved to a local **SQL Server** database, enabling efficient data management.
- **Student Listing**: View a list of registered students and search by their registration ID.

## 🛠️ Technologies Used

- **Language**: C#
- **Framework**: .NET 6 or higher
- **Database**: SQL Server (can be replaced with MySQL, SQLite, etc.)
- **ZIP Code API**: Public API such as [ViaCEP](https://viacep.com.br/)
- **Libraries**: Dapper, Dependency Injection, HTTP Client

