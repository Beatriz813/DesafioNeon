using Processador.Deposito.Core.Entidades;
using Processador.Deposito.Core.Enums;

namespace Processador.Deposito.Core.Ports.Adapters.Mongo.Repository
{
    public interface ILogRepository
    {
        ValueTask RegistrarLog(TransacaoDeposito transacao, string erro, EnumStatus status);
        public ValueTask<List<LogTransacao>> RecuperaTodosLogs();
        public ValueTask<List<LogTransacao>> RecuperaLogsPorStatus(EnumStatus status);
        public ValueTask<List<LogTransacao>> RecuperaLogsPorId(string idTransacao);
    }
}
