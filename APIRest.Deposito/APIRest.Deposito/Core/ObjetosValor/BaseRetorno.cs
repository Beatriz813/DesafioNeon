using APIRest.Deposito.Core.Enums;

namespace APIRest.Deposito.Core.ObjetosValor
{
    public struct BaseRetorno
    {
        public EnumStatus Status { get; set; }
        public string Retorno { get; set; }

        public BaseRetorno(string retorno, EnumStatus status = EnumStatus.SUCESSO)
        {
            Retorno = retorno;
            Status = status;
        }


    }


}
