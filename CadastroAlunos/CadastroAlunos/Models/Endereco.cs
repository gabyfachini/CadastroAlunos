using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CadastroAlunos.Models
{
    public class Endereco
    {
        [JsonPropertyName("cep")] // Especificar o nome exato do campo JSON para evitar erros
        public string Cep { get; set; }

        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; }

        [JsonPropertyName("complemento")]
        public string Complemento { get; set; }

        [JsonPropertyName("unidade")]
        public string Unidade { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }

        [JsonPropertyName("localidade")]
        public string Localidade { get; set; }

        [JsonPropertyName("uf")]
        public string UF { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; }

        [JsonPropertyName("regiao")]
        public string Regiao { get; set; }

        [JsonPropertyName("ibge")]
        public string IBGE { get; set; }

        [JsonPropertyName("gia")]
        public string GIA { get; set; }

        [JsonPropertyName("ddd")]
        public string DDD { get; set; }

        [JsonPropertyName("siafi")]
        public string SIAFI { get; set; }
    }
}
