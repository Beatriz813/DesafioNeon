using Processador.Deposito.Core.Entidades;
using Processador.Deposito.Core.ObjetosValor;

namespace Processador.Deposito.Core.Ports.UseCase
{
    public interface IUSCProcessar
    {
        ValueTask Processar();
        BaseRetorno ValidaDeposito(TransacaoDeposito transacao);
    }
}
