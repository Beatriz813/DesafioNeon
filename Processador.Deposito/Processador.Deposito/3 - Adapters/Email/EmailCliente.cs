using Processador.Deposito.Core.Entidades;
using Processador.Deposito.Core.ObjetosValor;
using Processador.Deposito.Core.Ports.Adapters.Email;

namespace Processador.Deposito.Adapters.Email
{
    public class EmailCliente : IEmailCliente
    {
        public ValueTask<BaseRetorno> EnviarEmail(TransacaoDeposito transacao)
        {
            Console.WriteLine($"Depósito feito pela agencia: {transacao.AgenciaOrigem} ; conta: {transacao.NumeroContaOrigem}");
            Console.WriteLine($"Depósito recebido pela agencia: {transacao.AgenciaDestino} ; conta: {transacao.NumeroContaDestino}");
            
            return new ValueTask<BaseRetorno>(new BaseRetorno("Email Enviado com sucesso"));
        }
    }
}
