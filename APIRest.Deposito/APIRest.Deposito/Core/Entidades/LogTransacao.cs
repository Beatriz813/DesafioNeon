using APIRest.Deposito.Core.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace APIRest.Deposito.Core.Entidades
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
