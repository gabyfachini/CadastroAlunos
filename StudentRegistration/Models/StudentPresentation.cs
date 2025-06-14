using StudentRegistration.DAL;
using StudentRegistration.Interfaces;
using StudentRegistration.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentRegistration.Models
{
    public class StudentPresentation
    {
        private readonly IStudentService _studentService;
        private readonly IViaCepService _viaCepService;
        private readonly IStudentRepository _studentRepository;

        public StudentPresentation(IStudentService studentService, IViaCepService viaCepService, IStudentRepository studentRepository)
        {
            _studentService = studentService;
            _viaCepService = viaCepService;
            _studentRepository = studentRepository;
        }

        public async Task RegisterStudents()
        {
            var newStudent = new Student(); // not recommended to use this directly

            newStudent.FirstName = GetValidInput("Enter the student's first name:", "First name must be between 2 and 100 characters and cannot be empty.");
            newStudent.LastName = GetValidInput("Enter the student's last name:", "Last name must be between 2 and 100 characters and cannot be empty.");
            newStudent.BirthDate = GetBirthDate("Enter the birth date | Format DD/MM/YYYY):");
            newStudent.Gender = GetValidGender("Enter gender (M/F):");
            newStudent.Email = GetValidEmail("Enter email:", "Invalid email!");
            newStudent.Phone = GetValidPhone("Enter phone number | Format (XX) 9XXXX-XXXX:", "Invalid phone number!");

            newStudent.LastUpdateDate = newStudent.RegistrationDate = DateTime.Now;
            newStudent.IsActive = true;

            Console.WriteLine("Enter the ZIP code:");
            newStudent.ZipCode = Console.ReadLine()?.Trim();

            try
            {
                var address = await GetAddressByZipCodeAsync(newStudent.ZipCode);

                if (address == null)
                {
                    Console.WriteLine("Registration not completed due to address error.");
                    return;
                }

                // Assign address data to student
                newStudent.Street = address.Street;
                newStudent.AddressComplement = string.IsNullOrWhiteSpace(address.AddressComplement) ? "N/A" : address.AddressComplement;
                newStudent.Neighborhood = address.Neighborhood;
                newStudent.City = address.City;
                newStudent.State = address.State;

                if (ValidateStudent(newStudent))
                {
                    await _studentRepository.RegisterStudentAsync(newStudent);
                    Console.WriteLine("Student successfully registered!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching address: {ex.Message}");
            }
            Thread.Sleep(5000); // menu appears before data
            /*Console.Clear();*/
        }

        public void ListStudents()
        {
            Console.Clear();
            Console.WriteLine("STUDENT REGISTRATION\n");

            List<Student> students = _studentService.ListStudents();

            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.Id}\nName: {student.FirstName} {student.LastName}\nBirth Date: {student.BirthDate}\nGender: {student.Gender}\nEmail: {student.Email}\nPhone: {student.Phone}\nAddress: {student.Street}, {student.AddressComplement}. {student.Neighborhood}, {student.City} - {student.State} (ZIP: {student.ZipCode})\n");
            }

            Console.WriteLine("Press any key to return to menu");
            Console.ReadKey();
            Console.Clear();
        }

        public void SearchStudent()
        {
            Console.WriteLine("STUDENT SEARCH\n");
            List<Student> students = _studentService.ListStudents();

            Console.Write("Enter the student ID: ");
            int studentId;
            if (int.TryParse(Console.ReadLine(), out studentId))
            {
                var student = students.FirstOrDefault(s => s.Id == studentId);

                if (student != null)
                {
                    Console.WriteLine($"ID: {student.Id}\nName: {student.FirstName} {student.LastName}\nBirth Date: {student.BirthDate}\nGender: {student.Gender}\nEmail: {student.Email}\nPhone: {student.Phone}\nAddress: {student.Street}, {student.AddressComplement}. {student.Neighborhood}, {student.City} - {student.State} (ZIP: {student.ZipCode})\n");
                }
                else
                {
                    Console.WriteLine("Student not found!");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID. Please try again.");
            }

            Console.WriteLine("Press any key to return to menu");
            Console.ReadKey();
            Console.Clear();
        }

        public void UpdateStudents()
        {
            // to be implemented
        }

        public void SoftDelete()
        {
            Console.WriteLine("STUDENT DELETION");
            string myConnectionString = DatabaseConnection.GetConnectionString("Default");

            while (true)
            {
                Console.WriteLine("Enter the ID of the student you want to delete (or 0 to cancel):");

                if (int.TryParse(Console.ReadLine(), out int chosenId))
                {
                    if (chosenId == 0)
                    {
                        Console.WriteLine("Student deletion canceled.");
                        break;
                    }

                    _studentService.SoftDelete(myConnectionString, chosenId);

                    Console.WriteLine($"Student with ID {chosenId} was successfully marked as inactive!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid ID. Please enter a valid number.");
                }
            }
            Console.Clear();
        }

        // Helper Methods:
        private string GetValidInput(string prompt, string errorMessage)
        {
            string input;

            do
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) || input.Length < 2 || input.Length > 100)
                {
                    Console.WriteLine(errorMessage);
                }
                else
                {
                    return input;
                }

            } while (true);
        }

        private DateTime GetBirthDate(string prompt)
        {
            DateTime date;
            string[] formats = { "dd/MM/yyyy" };
            do
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (!DateTime.TryParse(input, out date))
                {
                    Console.WriteLine("Invalid birth date! Please try again.");
                }
                else
                {
                    return date;
                }

            } while (true);
        }

        private char GetValidGender(string prompt)
        {
            string gender;
            do
            {
                Console.WriteLine(prompt);
                gender = Console.ReadLine().Trim().ToUpper();

                if (gender != "M" && gender != "F")
                {
                    Console.WriteLine("Invalid entry. Enter 'M' for male or 'F' for female.");
                }

            } while (gender != "M" && gender != "F");

            return char.Parse(gender);
        }

        private string GetValidEmail(string prompt, string errorMessage)
        {
            string email;
            string emailPattern = @"^[^@\s]+@[^@\s]+\.(com|com\.br|br)$"; // Regex to validate email

            do
            {
                Console.WriteLine(prompt);
                email = Console.ReadLine().Trim();

                if (!Regex.IsMatch(email, emailPattern, RegexOptions.IgnoreCase))
                {
                    Console.WriteLine(errorMessage);
                }
                else
                {
                    return email;
                }

            } while (true);
        }

        private string GetValidPhone(string prompt, string errorMessage)
        {
            string phone;
            string phonePattern = @"^\(\d{2}\) 9\d{4}-\d{4}$"; // Regex for Brazilian phone

            do
            {
                Console.WriteLine(prompt);
                phone = Console.ReadLine().Trim();

                if (!Regex.IsMatch(phone, phonePattern))
                {
                    Console.WriteLine(errorMessage);
                }
                else
                {
                    return phone;
                }

            } while (true);
        }

        public async Task<Address> GetAddressByZipCodeAsync(string zipCode)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
            {
                throw new ArgumentException("Invalid ZIP code.");
            }

            var address = await _viaCepService.FetchAddressByZipCodeAsync(zipCode);

            if (address == null)
            {
                throw new Exception("Address not found.");
            }

            return address;
        }

        private bool ValidateStudent(Student student)
        {
            var validationContext = new ValidationContext(student);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(student, validationContext, validationResults, true);

            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    Console.WriteLine($"Validation error: {validationResult.ErrorMessage}");
                }
            }
            return isValid;
        }
    }
}
