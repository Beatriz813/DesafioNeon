using Processador.Deposito.Core.Entidades;
using Processador.Deposito.Core.Enums;
using Processador.Deposito.Core.ObjetosValor;
using Processador.Deposito.Core.ObjetosValor.Exceptions;
using Processador.Deposito.Core.Ports.Adapters.Email;
using Processador.Deposito.Core.Ports.Adapters.HttpCliente;
using Processador.Deposito.Core.Ports.Adapters.Mongo.Repository;
using System.Text.Json;
using System.Text.RegularExpressions;

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

                var validacao = ValidaDeposito(transacao);
                if (validacao.Status is not EnumStatus.SUCESSO)
                {
                    await RegistraLogErroAsync(transacao, validacao.Retorno, EnumStatus.NEGOCIO);
                    continue;
                }

                EfetivarDeposito(transacao);
            }
        }

        public BaseRetorno ValidaDeposito(TransacaoDeposito transacao)
        {
            if (transacao.ContaClienteOrigem is null)
                return new BaseRetorno("Conta de origem inexistente", Enums.EnumStatus.NEGOCIO);
            if (transacao.ContaClienteDestino is null)
                return new BaseRetorno("Conta de destino inexistente", Enums.EnumStatus.NEGOCIO);

            if (transacao.Nome != transacao.ContaClienteDestino.Nome)
                return new BaseRetorno("Nome do recebedor informado não é igual ao cadastrado", Enums.EnumStatus.NEGOCIO);

            bool numerosAgenciaComCaractere = Regex.IsMatch(transacao.AgenciaDestino, "[^0-9]");

            bool numerosContaComCaractere = Regex.IsMatch(transacao.NumeroContaDestino, "[^0-9]");

            if (String.IsNullOrWhiteSpace(transacao.AgenciaDestino) || numerosAgenciaComCaractere)
                return new BaseRetorno("Agencia de destino inválida", Enums.EnumStatus.NEGOCIO);

            if (String.IsNullOrWhiteSpace(transacao.NumeroContaDestino) || numerosContaComCaractere)
                return new BaseRetorno("Conta de destino inválida.", Enums.EnumStatus.NEGOCIO);

            bool numerosAgenciaOrigemComCaractere = Regex.IsMatch(transacao.AgenciaOrigem, "[^0-9]");

            bool numerosContaOrigemComCaractere = Regex.IsMatch(transacao.NumeroContaOrigem, "[^0-9]");

            if (String.IsNullOrWhiteSpace(transacao.AgenciaOrigem) || numerosAgenciaOrigemComCaractere)
                return new BaseRetorno("Agencia de origem inválida", Enums.EnumStatus.NEGOCIO);

            if (String.IsNullOrWhiteSpace(transacao.NumeroContaOrigem) || numerosContaOrigemComCaractere)
                return new BaseRetorno("Conta de origem inválida.", Enums.EnumStatus.NEGOCIO);

            if (transacao.Valor <= 0)
                return new BaseRetorno($"O valor {transacao.Valor} não pode ser depositado.", Enums.EnumStatus.NEGOCIO);
            
            return new BaseRetorno("Validação feita com sucesso");
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
