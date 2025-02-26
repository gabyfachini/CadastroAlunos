﻿using System;
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

        var alunoPresentation = serviceProvider.GetService<AlunoPresentation>();

        Console.WriteLine("GERENCIADOR DE ALUNOS");
        Console.WriteLine();

        while (true)
        {
            Menu();
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    alunoPresentation.RegisterStudents();
                    break;

                case "2":
                    alunoPresentation.StudentList(); 
                    break;

                case "3":
                    alunoPresentation.StudentSearch();
                    break;

                case "4":
                    alunoPresentation.UpdateStudents();
                    break;

                case "5":
                    alunoPresentation.SoftDelete();
                    break;

                case "0":
                    Console.WriteLine("O programa está prestes a ser encerrado.");
                    return; // Finaliza o método Main, encerrando o programa

                default:
                    Console.WriteLine("Opção inválida!");
                    Thread.Sleep(500); //Agurda 0.5s para limpar e pedir o menu novamente
                    Console.Clear();
                    continue; // Continua pedindo a opção
            }
        }
    }
    private static void Menu()
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
        serviceProvider = new ServiceCollection() //Coleção de serviços para registrar no contêiner de DI
            .AddSingleton<IConfiguration>(new ConfigurationBuilder() //instancia compartilhada entre todos os componentes, usada sempre
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build())
            .AddScoped<IAlunoRepository, AlunoRepository>() //criados por solicitação, sempre sera uma nova instância para cada requisição
            .AddScoped<IAlunoService, AlunoService>() //criados por solicitação
            .AddScoped<AlunoPresentation>() //criados por solicitação
            .AddScoped<IViaCepService, ViaCepService>()
            .AddHttpClient()
            .BuildServiceProvider(); //cria o provedor de serviços, que é o que vai gerenciar a criação e a resolução dos serviços no restante do código
    }
}