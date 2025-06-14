using StudentRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistration.Interfaces
{
    public interface IStudentRepository
    {
        Task RegisterStudentAsync(Student newStudent);
        List<Student> GetList(); // Applies to case 2 and 3
        Task SoftDelete(int id); // Soft delete
    }
}
