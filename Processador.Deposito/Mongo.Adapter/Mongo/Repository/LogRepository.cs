using MongoDB.Driver;
using Processador.Deposito.Core.Entidades;
using Processador.Deposito.Core.Enums;
using Processador.Deposito.Core.Ports.Adapters.Mongo.Repository;

namespace Processador.Deposito.Adapters.Mongo.Repository
{
    public class LogRepository : ILogRepository
    {
        private IMongoCollection<LogTransacao> _logTransacao;
        public LogRepository()
        {
            var clienteMongo = new MongoClient("mongodb://userNeon:adminWEB123#@localhost:27017/?authSource=admin");
            _logTransacao = clienteMongo.GetDatabase("Neon").GetCollection<LogTransacao>("LogTransacao");
        }
        public async ValueTask RegistrarLog(TransacaoDeposito transacao, string erro, EnumStatus status)
        {
            var log = new LogTransacao
            {
                Transacao = transacao,
                Erro = erro,
                Status = status
            };
            await _logTransacao.InsertOneAsync(log);
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
