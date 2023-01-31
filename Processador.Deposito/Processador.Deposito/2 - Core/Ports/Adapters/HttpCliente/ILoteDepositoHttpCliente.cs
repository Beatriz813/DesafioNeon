using Processador.Deposito.Core.Entidades;
using Processador.Deposito.Core.ObjetosValor;

namespace Processador.Deposito.Core.Ports.Adapters.HttpCliente
{
    public interface ILoteDepositoHttpCliente
    {
        public ValueTask<BaseRetorno> RecuperarLoteTransacoes();
    }
}
