using Processador.Deposito.Core.ObjetosValor;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Processador.Deposito.Core.Entidades
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

        public BaseRetorno ValidaDeposito()
        {
            if (this.ContaClienteOrigem is null)
                return new BaseRetorno("Conta de origem inexistente", Enums.EnumStatus.NEGOCIO);
            if (this.ContaClienteDestino is null)
                return new BaseRetorno("Conta de destino inexistente", Enums.EnumStatus.NEGOCIO);

            if (this.Nome != this.ContaClienteDestino.Nome)
                return new BaseRetorno("Nome do recebedor informado não é igual ao cadastrado", Enums.EnumStatus.NEGOCIO);

            bool numerosAgenciaComCaractere = Regex.IsMatch(this.AgenciaDestino, "[^0-9]");

            bool numerosContaComCaractere = Regex.IsMatch(this.NumeroContaDestino, "[^0-9]");

            if (String.IsNullOrWhiteSpace(this.AgenciaDestino) || numerosAgenciaComCaractere)
                return new BaseRetorno("Agencia de destino inválida", Enums.EnumStatus.NEGOCIO);

            if (String.IsNullOrWhiteSpace(this.NumeroContaDestino) || numerosContaComCaractere)
                return new BaseRetorno("Conta de destino inválida.", Enums.EnumStatus.NEGOCIO);

            bool numerosAgenciaOrigemComCaractere = Regex.IsMatch(this.AgenciaOrigem, "[^0-9]");

            bool numerosContaOrigemComCaractere = Regex.IsMatch(this.NumeroContaOrigem, "[^0-9]");

            if (String.IsNullOrWhiteSpace(this.AgenciaOrigem) || numerosAgenciaOrigemComCaractere)
                return new BaseRetorno("Agencia de origem inválida", Enums.EnumStatus.NEGOCIO);

            if (String.IsNullOrWhiteSpace(this.NumeroContaOrigem) || numerosContaOrigemComCaractere)
                return new BaseRetorno("Conta de origem inválida.", Enums.EnumStatus.NEGOCIO);

            if (this.Valor <= 0)
                return new BaseRetorno($"O valor {this.Valor} não pode ser depositado.", Enums.EnumStatus.NEGOCIO);

            return new BaseRetorno("Validação feita com sucesso");
        }
    }
}
