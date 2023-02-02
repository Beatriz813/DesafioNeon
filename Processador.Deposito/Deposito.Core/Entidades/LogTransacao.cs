using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Processador.Deposito.Core.Enums;

namespace Processador.Deposito.Core.Entidades
{
    public class LogTransacao
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public TransacaoDeposito Transacao { get; set; }
        public string Erro { get; set; }
        public EnumStatus Status { get; set; }
    }
}
