using Processador.Deposito.Core.Entidades;
using Processador.Deposito.Core.ObjetosValor;

namespace Processador.Deposito.Core.Ports.Adapters.HttpCliente
{
    public interface IContasHttpCliente
    {
        ValueTask<List<Conta>> RecuperarContas();
        Conta? RecuperaConta(string agencia, string conta);
    }
}
