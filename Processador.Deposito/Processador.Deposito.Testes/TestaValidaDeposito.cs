using Microsoft.Extensions.DependencyInjection;
using Processador.Deposito.Core.Entidades;
using Processador.Deposito.Core.Enums;
using Processador.Deposito.Core.ObjetosValor;
using Processador.Deposito.Core.Ports.UseCase;

namespace Processador.Deposito.Testes
{
    public class TestaValidaDeposito
    {
        private readonly IUSCProcessar _uscProcessar;
        public TestaValidaDeposito()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.RegistraServicos();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _uscProcessar = serviceProvider.GetRequiredService<IUSCProcessar>();
        }
        [Fact]
        public async void ContaOrigemInexistenteRetornaErroNegocio ()
        {

            TransacaoDeposito transacao = new TransacaoDeposito()
            {
                Id = "1d9b4f37-7fac-467f-a1e1-b33ec2e8025f",
                Nome = "Luana Souza",
                AgenciaDestino = "0001",
                NumeroContaDestino = "123456",
                AgenciaOrigem = "0867",
                NumeroContaOrigem = "654321",
                Valor = 130.45M,
                DataTransacao = new DateTime(2021, 11, 09),
                ContaClienteDestino = new Conta()
                {
                    Id = "a57b601a-dc05-466b-94a2-4087a350dc45",
                    Agencia = "0001",
                    NumeroConta = "123456",
                    Nome = "Luana Souza"
                },
                ContaClienteOrigem = null
            };

            BaseRetorno retorno = await _uscProcessar.ValidaDepositoAsync(transacao);

            Assert.True(retorno.Status == EnumStatus.NEGOCIO);
        }

        [Fact]
        public async void ContaDestinoInexistenteRetornaErroNegocio()
        {

            TransacaoDeposito transacao = new TransacaoDeposito()
            {
                Id = "1d9b4f37-7fac-467f-a1e1-b33ec2e8025f",
                Nome = "Luana Souza",
                AgenciaDestino = "0001",
                NumeroContaDestino = "123456",
                AgenciaOrigem = "0867",
                NumeroContaOrigem = "654321",
                Valor = 130.45M,
                DataTransacao = new DateTime(2021, 11, 09),
                ContaClienteDestino = null,
                ContaClienteOrigem = new Conta()
                {
                    Id = "3663b0b7-eff9-454c-b9a6-40162e386871",
                    Agencia = "0867",
                    NumeroConta = "654321",
                    Nome = "Marcos Silva"
                }
            };

            BaseRetorno retorno = await _uscProcessar.ValidaDepositoAsync(transacao);

            Assert.True(retorno.Status == EnumStatus.NEGOCIO);
        }

        [Fact]
        public async void NomeInformadoDiferenteDaContaDestinoCadastradaRetornaErroNegocio()
        {

            TransacaoDeposito transacao = new TransacaoDeposito()
            {
                Id = "1d9b4f37-7fac-467f-a1e1-b33ec2e8025f",
                Nome = "Luana Sousa",
                AgenciaDestino = "0001",
                NumeroContaDestino = "123456",
                AgenciaOrigem = "0867",
                NumeroContaOrigem = "654321",
                Valor = 130.45M,
                DataTransacao = new DateTime(2021, 11, 09),
                ContaClienteDestino = new Conta()
                {
                    Id = "a57b601a-dc05-466b-94a2-4087a350dc45",
                    Agencia = "0001",
                    NumeroConta = "123456",
                    Nome = "Luana Souza"
                },
                ContaClienteOrigem = new Conta()
                {
                    Id = "3663b0b7-eff9-454c-b9a6-40162e386871",
                    Agencia = "0867",
                    NumeroConta = "654321",
                    Nome = "Marcos Silva"
                }
            };

            BaseRetorno retorno = await _uscProcessar.ValidaDepositoAsync(transacao);

            Assert.True(retorno.Status == EnumStatus.NEGOCIO);
        }

        [Fact]
        public async void AgenciaDestinoInvalidaRetornaErroNegocio()
        {

            TransacaoDeposito transacao = new TransacaoDeposito()
            {
                Id = "1d9b4f37-7fac-467f-a1e1-b33ec2e8025f",
                Nome = "Luana Souza",
                AgenciaDestino = "0001A",
                NumeroContaDestino = "123456",
                AgenciaOrigem = "0867",
                NumeroContaOrigem = "654321",
                Valor = 130.45M,
                DataTransacao = new DateTime(2021, 11, 09),
                ContaClienteDestino = new Conta()
                {
                    Id = "a57b601a-dc05-466b-94a2-4087a350dc45",
                    Agencia = "0001",
                    NumeroConta = "123456",
                    Nome = "Luana Souza"
                },
                ContaClienteOrigem = new Conta()
                {
                    Id = "3663b0b7-eff9-454c-b9a6-40162e386871",
                    Agencia = "0867",
                    NumeroConta = "654321",
                    Nome = "Marcos Silva"
                }
            };

            BaseRetorno retorno = await _uscProcessar.ValidaDepositoAsync(transacao);

            Assert.True(retorno.Status == EnumStatus.NEGOCIO && retorno.Retorno == "Agencia de destino inválida");
        }

        [Fact]
        public async void ContaDestinoInvalidaRetornaErroNegocio()
        {

            TransacaoDeposito transacao = new TransacaoDeposito()
            {
                Id = "1d9b4f37-7fac-467f-a1e1-b33ec2e8025f",
                Nome = "Luana Souza",
                AgenciaDestino = "0001",
                NumeroContaDestino = "123456sd",
                AgenciaOrigem = "0867",
                NumeroContaOrigem = "654321",
                Valor = 130.45M,
                DataTransacao = new DateTime(2021, 11, 09),
                ContaClienteDestino = new Conta()
                {
                    Id = "a57b601a-dc05-466b-94a2-4087a350dc45",
                    Agencia = "0001",
                    NumeroConta = "123456",
                    Nome = "Luana Souza"
                },
                ContaClienteOrigem = new Conta()
                {
                    Id = "3663b0b7-eff9-454c-b9a6-40162e386871",
                    Agencia = "0867",
                    NumeroConta = "654321",
                    Nome = "Marcos Silva"
                }
            };

            BaseRetorno retorno = await _uscProcessar.ValidaDepositoAsync(transacao);

            Assert.True(retorno.Status == EnumStatus.NEGOCIO && retorno.Retorno == "Conta de destino inválida.");
        }

        [Fact]
        public async void ContaOrigemInvalidaRetornaErroNegocio()
        {

            TransacaoDeposito transacao = new TransacaoDeposito()
            {
                Id = "1d9b4f37-7fac-467f-a1e1-b33ec2e8025f",
                Nome = "Luana Souza",
                AgenciaDestino = "0001",
                NumeroContaDestino = "123456",
                AgenciaOrigem = "0867",
                NumeroContaOrigem = "654321kj",
                Valor = 130.45M,
                DataTransacao = new DateTime(2021, 11, 09),
                ContaClienteDestino = new Conta()
                {
                    Id = "a57b601a-dc05-466b-94a2-4087a350dc45",
                    Agencia = "0001",
                    NumeroConta = "123456",
                    Nome = "Luana Souza"
                },
                ContaClienteOrigem = new Conta()
                {
                    Id = "3663b0b7-eff9-454c-b9a6-40162e386871",
                    Agencia = "0867",
                    NumeroConta = "654321",
                    Nome = "Marcos Silva"
                }
            };

            BaseRetorno retorno = await _uscProcessar.ValidaDepositoAsync(transacao);

            Assert.True(retorno.Status == EnumStatus.NEGOCIO && retorno.Retorno == "Conta de origem inválida.");
        }

        [Fact]
        public async void AgenciaOrigemInvalidaRetornaErroNegocio()
        {

            TransacaoDeposito transacao = new TransacaoDeposito()
            {
                Id = "1d9b4f37-7fac-467f-a1e1-b33ec2e8025f",
                Nome = "Luana Souza",
                AgenciaDestino = "0001",
                NumeroContaDestino = "123456",
                AgenciaOrigem = "0867sdf",
                NumeroContaOrigem = "654321",
                Valor = 130.45M,
                DataTransacao = new DateTime(2021, 11, 09),
                ContaClienteDestino = new Conta()
                {
                    Id = "a57b601a-dc05-466b-94a2-4087a350dc45",
                    Agencia = "0001",
                    NumeroConta = "123456",
                    Nome = "Luana Souza"
                },
                ContaClienteOrigem = new Conta()
                {
                    Id = "3663b0b7-eff9-454c-b9a6-40162e386871",
                    Agencia = "0867",
                    NumeroConta = "654321",
                    Nome = "Marcos Silva"
                }
            };

            BaseRetorno retorno = await _uscProcessar.ValidaDepositoAsync(transacao);

            Assert.True(retorno.Status == EnumStatus.NEGOCIO && retorno.Retorno == "Agencia de origem inválida");
        }

        [Fact]
        public async void ValorNegativoRetornaErroNegocio()
        {

            TransacaoDeposito transacao = new TransacaoDeposito()
            {
                Id = "1d9b4f37-7fac-467f-a1e1-b33ec2e8025f",
                Nome = "Luana Souza",
                AgenciaDestino = "0001",
                NumeroContaDestino = "123456",
                AgenciaOrigem = "0867",
                NumeroContaOrigem = "654321",
                Valor = -130.45M,
                DataTransacao = new DateTime(2021, 11, 09),
                ContaClienteDestino = new Conta()
                {
                    Id = "a57b601a-dc05-466b-94a2-4087a350dc45",
                    Agencia = "0001",
                    NumeroConta = "123456",
                    Nome = "Luana Souza"
                },
                ContaClienteOrigem = new Conta()
                {
                    Id = "3663b0b7-eff9-454c-b9a6-40162e386871",
                    Agencia = "0867",
                    NumeroConta = "654321",
                    Nome = "Marcos Silva"
                }
            };

            BaseRetorno retorno = await _uscProcessar.ValidaDepositoAsync(transacao);

            Assert.True(retorno.Status == EnumStatus.NEGOCIO && retorno.Retorno == $"O valor {transacao.Valor} não pode ser depositado.");
        }
    }
}
