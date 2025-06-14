using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentRegistration.Models;
using StudentRegistration.DAL;
using StudentRegistration.Interfaces;
using StudentRegistration.Services;

internal class Program
{
    private static ServiceProvider serviceProvider;

    private static async Task Main(string[] args)
    {
        ConfigureServices(); // Dependency injection configuration

        var studentPresentation = serviceProvider.GetService<StudentPresentation>();

        Console.WriteLine("STUDENT MANAGER");
        Console.WriteLine();

        while (true)
        {
            ShowMenu();
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    studentPresentation.RegisterStudents();
                    break;

                case "2":
                    studentPresentation.StudentList();
                    break;

                case "3":
                    studentPresentation.StudentSearch();
                    break;

                case "4":
                    studentPresentation.UpdateStudents();
                    break;

                case "5":
                    studentPresentation.SoftDelete();
                    break;

                case "0":
                    Console.WriteLine("The program is about to close.");
                    return; // Ends the Main method, closing the program

                default:
                    Console.WriteLine("Invalid option!");
                    Thread.Sleep(500); // Waits 0.5s before clearing and showing the menu again
                    Console.Clear();
                    continue; // Continue requesting option input
            }
        }
    }

    private static void ShowMenu()
    {
        Console.WriteLine("1. Register Student");
        Console.WriteLine("2. List Students");
        Console.WriteLine("3. Search Student");
        Console.WriteLine("4. Update Student");
        Console.WriteLine("5. Delete Student");
        Console.WriteLine("0. Exit");
        Console.Write("Selected option: ");
    }

    private static void ConfigureServices() // Dependency injection method
    {
        serviceProvider = new ServiceCollection() // Service collection to register in the DI container
            .AddSingleton<IConfiguration>(new ConfigurationBuilder() // Shared instance across all components, always used
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build())
            .AddScoped<IStudentRepository, StudentRepository>() // Created per request, new instance for each request
            .AddScoped<IStudentService, StudentService>() // Created per request
            .AddScoped<StudentPresentation>() // Created per request
            .AddScoped<IViaCepService, ViaCepService>()
            .AddHttpClient()
            .BuildServiceProvider(); // Creates the service provider which manages the creation and resolution of services in the rest of the code
    }
}