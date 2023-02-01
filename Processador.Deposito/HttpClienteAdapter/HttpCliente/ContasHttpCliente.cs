using Processador.Deposito.Core.Entidades;
using Processador.Deposito.Core.Enums;
using Processador.Deposito.Core.ObjetosValor;
using Processador.Deposito.Core.ObjetosValor.Exceptions;
using Processador.Deposito.Core.Ports.Adapters.HttpCliente;
using System.Net.Http.Json;
using System.Text.Json;

namespace Processador.Deposito.Adapters.HttpCliente
{
    public class ContasHttpCliente : IContasHttpCliente
    {
        private List<Conta> contas;
        public ContasHttpCliente()
        {
            contas = RecuperarContas().Result;
        }
        public Conta? RecuperaConta(string agencia, string numeroConta)
        {
            return contas.FirstOrDefault(conta => conta.Agencia == agencia && conta.NumeroConta == numeroConta);
        }

        public async ValueTask<List<Conta>> RecuperarContas()
        {
            var cliente = new HttpClient()
            {
                BaseAddress = new Uri("https://run.mocky.io/v3/7f0acd4b-e63d-4571-b834-c3db15f70673")
            };
            var resposta = await cliente.GetFromJsonAsync<List<Conta>>("");

            if (resposta == null)
                throw new NegocioException("Não há contas cadastradas.");

            return resposta;
        }
    }
}
