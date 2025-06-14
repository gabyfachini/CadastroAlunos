using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistration.Models
{
    public class Student
    {
        public int Id { get; set; } // Required in the Database

        [Required(ErrorMessage = "First name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 100 characters")]
        public string FirstName { get; set; } // Required in the Database

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 100 characters")]
        public string LastName { get; set; } // Required in the Database

        [Required(ErrorMessage = "Birth date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid birth date")]
        public DateTime BirthDate { get; set; } // Required in the Database

        [Required(ErrorMessage = "Gender is required")]
        [RegularExpression("^[MF]$", ErrorMessage = "Gender must be 'M' for Male or 'F' for Female")]
        public char Gender { get; set; } // Required in the Database

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        public DateTime RegistrationDate { get; set; } // Required in the Database
        public string Phone { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string AddressComplement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        
        public DateTime? LastUpdateDate { get; set; } // AUDIT FIELD - Date of last update (nullable)
        public bool IsActive { get; set; } // AUDIT FIELD | true = active, false = deleted | Required in the Database
    }
}
