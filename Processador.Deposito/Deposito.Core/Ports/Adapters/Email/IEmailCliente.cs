using Processador.Deposito.Core.Entidades;
using Processador.Deposito.Core.ObjetosValor;

namespace Processador.Deposito.Core.Ports.Adapters.Email
{
    public interface IEmailCliente
    {
        ValueTask<BaseRetorno> EnviarEmail(TransacaoDeposito transacao);
    }
}
