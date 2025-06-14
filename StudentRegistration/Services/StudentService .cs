using StudentRegistration.Interfaces;
using StudentRegistration.Models;
using StudentRegistration.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;  
using Dapper;

namespace StudentRegistration.Services
{
    public class StudentService : IStudentService // Responsible for operations like RegisterStudent and other data manipulations.
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IViaCepService _viaCepService;

        public StudentService(IStudentRepository studentRepository, IViaCepService? viaCepService = null)
        {
            _studentRepository = studentRepository;
            _viaCepService = viaCepService;
        }

        public async Task<Address> GetAddressByZipCodeAsync(string zipCode, Student student)
        {
            // Validate the ZIP code
            if (string.IsNullOrWhiteSpace(zipCode))
            {
                throw new ArgumentException("Invalid ZIP code.");
            }

            // Fetch the address using the ViaCepService (or other service)
            var address = await _viaCepService.FetchAddressByZipCodeAsync(zipCode);

            // Check if the address was found
            if (address == null)
            {
                throw new Exception("Address not found.");
            }

            // If the address is valid, register the student
            await _studentRepository.RegisterStudentAsync(student);

            // Return the address after registration
            return address;
        }

        public List<Student> ListStudents()
        {
            return _studentRepository.GetList();
        }

        public void SoftDelete(string connectionString, int studentId)
        {
            _studentRepository.SoftDelete(studentId);
        }
    }
}
