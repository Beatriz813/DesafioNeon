using Processador.Deposito.Core.Entidades;
using Processador.Deposito.Core.Enums;
using Processador.Deposito.Core.ObjetosValor;
using Processador.Deposito.Core.Ports.Adapters.HttpCliente;
using System.Net.Http.Json;
using System.Text.Json;

namespace Processador.Deposito.Adapters.HttpCliente
{
    public class LoteDepositoHttpCliente : ILoteDepositoHttpCliente
    {
        public async ValueTask<BaseRetorno> RecuperarLoteTransacoes()
        {
            var cliente = new HttpClient()
            {
                BaseAddress = new Uri("https://run.mocky.io/v3/68cc9f8b-519b-4057-bf3c-804115e68fd4")
            };
            var resposta = await cliente.GetFromJsonAsync<List<TransacaoDeposito>>("");
            
            if (resposta == null)
                return new BaseRetorno("Não há transações para realizar.", EnumStatus.NEGOCIO);
            
            return new BaseRetorno(JsonSerializer.Serialize(resposta));
        }
    }

    public static class ClienteExtension
    {
        public static void WriteRequestToConsole(this HttpResponseMessage response)
        {
            if (response is null)
            {
                return;
            }

            var request = response.RequestMessage;
            Console.WriteLine($"{request?.Method} ");
            Console.WriteLine($"{request?.RequestUri} ");
            Console.WriteLine($"HTTP/{request?.Version}");
        }
    }
}
