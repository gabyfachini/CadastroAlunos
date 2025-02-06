using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CadastroAlunos.Models;
using CadastroAlunos.DAL;
using CadastroAlunos.Interfaces;
using CadastroAlunos.Services;

internal class Program
{
    private static ServiceProvider serviceProvider;
    private static async Task Main(string[] args)
    {
        ConfigureServices(); // Configuração da injeção de dependência

        var alunoPresentation = serviceProvider.GetService<StudentPresentation>();

        Console.WriteLine("GERENCIADOR DE ALUNOS");
        Console.WriteLine();

        while (true)
        {
            ExibirMenu();
            string opcao = Console.ReadLine();
            Console.WriteLine();

            switch (opcao)
            {
                case "1":

                    break;

                case "2":
                    alunoPresentation?.StudentList(); // Garantir que não é nulo
                    break;

                case "3":
                    
                    break;

                case "4":

                    break;

                case "0":
                    Console.WriteLine("O programa está prestes a ser encerrado.");
                    return; // Finaliza o método Main, encerrando o programa

                default:
                    Console.WriteLine("Opção inválida!");
                    continue; // Continua pedindo a opção
            }
        }
    }
    private static void ExibirMenu()
    {
        Console.WriteLine("1. Cadastrar Aluno");
        Console.WriteLine("2. Listar Alunos");
        Console.WriteLine("3. Buscar Aluno");
        Console.WriteLine("4. Atualizar Aluno");
        Console.WriteLine("5. Excluir Aluno");
        Console.WriteLine("0. Sair");
        Console.Write("Opção escolhida: ");
    }
    private static void ConfigureServices() //Método da injeção de dependência
    {
        serviceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(new ConfigurationBuilder() //instancia compartilhada entre todos os componentes, usada sempre
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build())
            .AddScoped<IAlunoRepository, AlunoDAL>() //criados por solicitação
            .AddScoped<IAlunoService, StudentService>() //criados por solicitação
            .AddScoped<StudentPresentation>() //criados por solicitação
            .BuildServiceProvider(); //cria o provedor de serviços
    }
}