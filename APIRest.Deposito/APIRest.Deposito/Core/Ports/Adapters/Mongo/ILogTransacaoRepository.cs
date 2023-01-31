using APIRest.Deposito.Core.Entidades;
using APIRest.Deposito.Core.Enums;

namespace APIRest.Deposito.Core.Ports.Adapters.Mongo
{
    public interface ILogTransacaoRepository
    {
        public ValueTask<List<LogTransacao>> RecuperaTodosLogs();
        public ValueTask<List<LogTransacao>> RecuperaLogsPorStatus(EnumStatus status);
        public ValueTask<List<LogTransacao>> RecuperaLogsPorId(string idTransacao);
    }
}
