using StudentRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistration.Interfaces
{
    public interface IViaCepService
    {
        Task<Address?> GetAddressByZipCodeAsync(string zipCode);
    }
}
