using CadastroAlunos.Interfaces;
using CadastroAlunos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CadastroAlunos.Services
{
    internal class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;
        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Endereco?> BuscarEnderecoPorCepAsync(string cep)
        {
            if (cep.Length != 8) //Considera endereços do Brasil
            {
                Console.WriteLine("CEP inválido! O CEP deve ter 8 caracteres.");
                return null;
            }

            try
            {
                var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    /*Console.WriteLine($"Conteúdo da resposta: {content}");*/ //Verificar o retorno da API para análise de erros
                    var endereco = JsonSerializer.Deserialize<Endereco>(content);

                    if (endereco == null)
                    {
                        Console.WriteLine("A deserialização falhou. O 'objeto' endereco é nulo.");
                        return null;
                    }

                    // Verificar o conteúdo das propriedades da classe Endereco após a deserialização, utilizei para verificação de erros
                    /*Console.WriteLine("Conteúdo de endereco após deserialização:");
                    Console.WriteLine($"CEP: {endereco.Cep}");
                    Console.WriteLine($"Logradouro: {endereco.Logradouro}");
                    Console.WriteLine($"Complemento: {endereco.Complemento}");
                    Console.WriteLine($"Bairro: {endereco.Bairro}");
                    Console.WriteLine($"Localidade: {endereco.Localidade}");
                    Console.WriteLine($"UF: {endereco.UF}");*/

                    if (string.IsNullOrEmpty(endereco.Cep))
                    {
                        Console.WriteLine("CEP está vazio ou nulo.");
                    }

                    return endereco;
                }
                else
                {
                    Console.WriteLine($"Erro na resposta da API: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar CEP: {ex.Message}");
                return null;
            }
        }
    }
}
