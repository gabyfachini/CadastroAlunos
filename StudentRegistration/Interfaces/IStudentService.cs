using StudentRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistration.Interfaces
{
    public interface IStudentService
    {
        Task<Address> GetAddressByZipCodeAsync(string zipCode, Student student); // case 1
        List<Student> GetStudentList(); // Applies to case 2 and 3
        void SoftDelete(string connectionString, int studentId); // case 5
    }
}
