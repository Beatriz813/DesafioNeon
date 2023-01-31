using System.Text.Json.Serialization;

namespace APIRest.Deposito.Core.Entidades
{
    public class Conta
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Agencia { get; set; }
        [JsonPropertyName("Conta")]
        public string NumeroConta { get; set; }
    }
}
