using APIRest.Deposito.Core.Entidades;
using APIRest.Deposito.Core.Enums;
using APIRest.Deposito.Core.Ports.Adapters.Mongo;
using MongoDB.Driver;

namespace APIRest.Deposito.Adapters.Mongo
{
    public class LogTransacaoRepository : ILogTransacaoRepository
    {
        private IMongoCollection<LogTransacao> _logTransacao;
        public LogTransacaoRepository()
        {
            var clienteMongo = new MongoClient("mongodb://userNeon:adminWEB123#@localhost:27017/?authSource=admin");
            _logTransacao = clienteMongo.GetDatabase("Neon").GetCollection<LogTransacao>("LogTransacao");
        }

        public async ValueTask<List<LogTransacao>> RecuperaLogsPorId(string idTransacao)
        {
            return (await _logTransacao.FindAsync(log => log.Transacao.Id == idTransacao)).ToList();
        }

        public async ValueTask<List<LogTransacao>> RecuperaLogsPorStatus(EnumStatus status)
        {
            return (await _logTransacao.FindAsync(log => log.Status == status)).ToList();
        }

        public async ValueTask<List<LogTransacao>> RecuperaTodosLogs()
        {
            return (await _logTransacao.FindAsync(l => true)).ToList();
        }
    }
}
