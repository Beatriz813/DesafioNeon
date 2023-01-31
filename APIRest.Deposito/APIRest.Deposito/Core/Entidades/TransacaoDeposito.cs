using System.Text.Json.Serialization;

namespace APIRest.Deposito.Core.Entidades
{
    public class TransacaoDeposito
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string AgenciaDestino { get; set; }
        [JsonPropertyName("ContaDestino")]
        public string NumeroContaDestino { get; set; }
        public string AgenciaOrigem { get; set; }
        [JsonPropertyName("ContaOrigem")]
        public string NumeroContaOrigem { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataTransacao { get; set; }
        public Conta? ContaClienteOrigem { get; set; }
        public Conta? ContaClienteDestino { get; set; }
    }
}
