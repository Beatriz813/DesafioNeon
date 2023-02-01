using Processador.Deposito.Core.Entidades;
using Processador.Deposito.Core.Enums;
using Processador.Deposito.Core.ObjetosValor;
using Processador.Deposito.Core.ObjetosValor.Exceptions;
using Processador.Deposito.Core.Ports.Adapters.Email;
using Processador.Deposito.Core.Ports.Adapters.HttpCliente;
using Processador.Deposito.Core.Ports.Adapters.Mongo.Repository;
using System.Text.Json;

namespace Processador.Deposito.Core.Ports.UseCase.Processar
{
    public class USCProcessar : IUSCProcessar
    {
        private ILoteDepositoHttpCliente _clienteTransacoesHttp;
        private IContasHttpCliente _clienteContasHttp;
        private IEmailCliente _clienteEmail;
        private ILogRepository _logRepository;
        public USCProcessar(ILoteDepositoHttpCliente transacoes, IContasHttpCliente contas, IEmailCliente email, ILogRepository logRepository)
        {
            _clienteTransacoesHttp = transacoes;
            _clienteContasHttp = contas;
            _clienteEmail = email;
            _logRepository = logRepository;
        }
        public async ValueTask Processar()
        {
            var retorno = await _clienteTransacoesHttp.RecuperarLoteTransacoes();
            if (retorno.Status is not Enums.EnumStatus.SUCESSO)
                throw new NegocioException(retorno.Retorno);

            var LoteTransacao = JsonSerializer.Deserialize<List<TransacaoDeposito>>(retorno.Retorno);

            foreach (var transacao in LoteTransacao)
            {
                transacao.ContaClienteOrigem = _clienteContasHttp.RecuperaConta(transacao.AgenciaOrigem, transacao.NumeroContaOrigem);
                transacao.ContaClienteDestino = _clienteContasHttp.RecuperaConta(transacao.AgenciaDestino, transacao.NumeroContaDestino);

                var validacao = transacao.ValidaDeposito();
                if (validacao.Status is not EnumStatus.SUCESSO)
                {
                    await RegistraLogErroAsync(transacao, validacao.Retorno, validacao.Status);
                    continue;
                }

                EfetivarDeposito(transacao);
            }
        }

        private async void EfetivarDeposito(TransacaoDeposito transacao)
        {
            BaseRetorno debitar = EfetivarDebito(transacao);
            if (debitar.Status is not Enums.EnumStatus.SUCESSO)
                await RegistraLogErroAsync(transacao, debitar.Retorno, debitar.Status);

            BaseRetorno creditar = EfetivarCredito(transacao);
            if (creditar.Status is not Enums.EnumStatus.SUCESSO)
                await RegistraLogErroAsync(transacao, creditar.Retorno, creditar.Status);

            BaseRetorno envioEmail = await _clienteEmail.EnviarEmail(transacao);
            if (envioEmail.Status is not Enums.EnumStatus.SUCESSO)
                await RegistraLogErroAsync(transacao, envioEmail.Retorno, envioEmail.Status);

            await RegistraLogSucessoAsync(transacao, "Transacao efetivada com sucesso");
        }


        private BaseRetorno EfetivarDebito(TransacaoDeposito transacao)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine($"Débito efetivado na conta");
            Console.WriteLine($"Conta: {transacao.NumeroContaOrigem} ; Agencia: {transacao.AgenciaOrigem}");
            Console.WriteLine("=============================================\n");
            return new BaseRetorno("Débito efetivado");
        }

        private BaseRetorno EfetivarCredito(TransacaoDeposito transacao)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine($"Crédito efetivado na conta");
            Console.WriteLine($"Conta: {transacao.NumeroContaDestino} ; Agencia: {transacao.AgenciaDestino}");
            Console.WriteLine("=============================================\n");
            return new BaseRetorno("Crédito efetivado");
        }

        private async Task RegistraLogErroAsync(TransacaoDeposito transacao, string mensagemErro, EnumStatus status)
        {
            await _logRepository.RegistrarLog(transacao, mensagemErro, status);
            Console.WriteLine("=============================================");
            Console.WriteLine($"ID Transacao: {transacao.Id}");
            Console.WriteLine($"Erro: {mensagemErro}");
            Console.WriteLine("=============================================\n");
        }

        private async Task RegistraLogSucessoAsync(TransacaoDeposito transacao, string mensagem)
        {
            await _logRepository.RegistrarLog(transacao, null, EnumStatus.SUCESSO);
            Console.WriteLine("=============================================");
            Console.WriteLine($"ID Transacao: {transacao.Id}");
            Console.WriteLine($"Mensagem: {mensagem}");
            Console.WriteLine("=============================================\n");
        }

    }
}
