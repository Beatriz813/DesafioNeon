using Processador.Deposito.Core.ObjetosValor;

namespace Processador.Deposito.Core.Ports.UseCase
{
    public interface IUSCProcessar
    {
        ValueTask Processar();
    }
}
