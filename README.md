# Student Registration System

## Overview

This is a **Student Registration System** built in C# using a clean architecture approach with dependency injection. The system allows you to register, list, search, update, and softly delete student records. It also integrates with the **ViaCep** API to automatically retrieve Brazilian address information based on postal codes (CEP).

---

## Features

- Register new students with validated inputs (name, surname, birthdate, gender, email, phone, and address).
- List all registered students.
- Search for a student by their ID.
- Update student information (function to be implemented).
- Soft delete a student (mark as inactive).
- Automatic address lookup using ViaCep API.
- Input validations for email, phone, dates, and other fields.
- Dependency Injection for better modularity and testability.

---

## Technologies Used

- .NET / C#
- Dependency Injection with Microsoft.Extensions.DependencyInjection
- HTTP Client for external API requests (ViaCep)
- JSON Serialization / Deserialization
- Regular Expressions for input validation
- Console application for user interaction

---

## How to Use

1. Run the application.
2. Use the menu to choose the operation you want:
   - Register a new student.
   - List all students.
   - Search for a student by ID.
   - Update student data (coming soon).
   - Soft delete a student.
   - Exit the program.

3. Follow the prompts to enter data when registering or searching students.

---

## Project Structure

- **Models**: Contains student and address models.
- **Services**: Business logic like student service and ViaCep address lookup service.
- **Repositories**: Data access layer handling storage and retrieval.
- **Presentation**: Console UI logic for interacting with users.
- **Program**: Application entry point, dependency injection setup, and main menu loop.

---

## Notes

- The update student function is planned but not yet implemented.
- The system assumes Brazilian postal codes (CEP) format.
- Soft delete only marks a student as inactive without removing data.
- The project uses asynchronous calls for external API requests.

---

## License

This project is open source and free to use.

---

## Author

Developed by Gabryella Fachini.
