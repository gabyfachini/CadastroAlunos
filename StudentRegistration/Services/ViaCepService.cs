using StudentRegistration.Interfaces;
using StudentRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentRegistration.Services
{
    internal class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;
        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Address?> FetchAddressByZipCodeAsync(string zipCode)
        {
            if (zipCode.Length != 8) // Considers Brazilian addresses
            {
                Console.WriteLine("Invalid ZIP code! The ZIP code must have 8 characters.");
                return null;
            }

            try
            {
                var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{zipCode}/json/");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    /*Console.WriteLine($"Response content: {content}");*/ // Check API return for error analysis
                    var address = JsonSerializer.Deserialize<Address>(content);

                    if (address == null)
                    {
                        Console.WriteLine("Deserialization failed. The 'address' object is null.");
                        return null;
                    }

                    // To verify the contents of the Address properties after deserialization, used for debugging
                    /*Console.WriteLine("Address content after deserialization:");
                    Console.WriteLine($"ZIP Code: {address.ZipCode}");
                    Console.WriteLine($"Street: {address.Street}");
                    Console.WriteLine($"Complement: {address.Complement}");
                    Console.WriteLine($"Neighborhood: {address.Neighborhood}");
                    Console.WriteLine($"City: {address.City}");
                    Console.WriteLine($"State: {address.State}");*/

                    if (string.IsNullOrEmpty(address.ZipCode))
                    {
                        Console.WriteLine("ZIP code is empty or null.");
                    }

                    return address;
                }
                else
                {
                    Console.WriteLine($"API response error: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ZIP code: {ex.Message}");
                return null;
            }
        }
    }
}
