using Microsoft.Extensions.DependencyInjection;
using Processador.Deposito.Core.Entidades;
using Processador.Deposito.Core.Enums;
using Processador.Deposito.Core.ObjetosValor;
using Processador.Deposito.Core.Ports.UseCase;

namespace Processador.Deposito.Testes
{
    public class TestaValidaDeposito
    {
        [Fact]
        public void ContaOrigemInexistenteRetornaErroNegocio ()
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

            BaseRetorno retorno =  transacao.ValidaDeposito();

            Assert.True(retorno.Status == EnumStatus.NEGOCIO && retorno.Retorno == "Conta de origem inexistente");
        }

        [Fact]
        public void ContaDestinoInexistenteRetornaErroNegocio()
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

            BaseRetorno retorno = transacao.ValidaDeposito();

            Assert.True(retorno.Status == EnumStatus.NEGOCIO && retorno.Retorno == "Conta de destino inexistente");
        }

        [Fact]
        public void NomeInformadoDiferenteDaContaDestinoCadastradaRetornaErroNegocio()
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

            BaseRetorno retorno = transacao.ValidaDeposito();

            Assert.True(retorno.Status == EnumStatus.NEGOCIO && retorno.Retorno == "Nome do recebedor informado não é igual ao cadastrado");
        }

        [Fact]
        public void AgenciaDestinoInvalidaRetornaErroNegocio()
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

            BaseRetorno retorno = transacao.ValidaDeposito();

            Assert.True(retorno.Status == EnumStatus.NEGOCIO && retorno.Retorno == "Agencia de destino inválida");
        }

        [Fact]
        public void AgenciaDestinoVaziaInvalidaRetornaErroNegocio()
        {

            TransacaoDeposito transacao = new TransacaoDeposito()
            {
                Id = "1d9b4f37-7fac-467f-a1e1-b33ec2e8025f",
                Nome = "Luana Souza",
                AgenciaDestino = "",
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

            BaseRetorno retorno = transacao.ValidaDeposito();

            Assert.True(retorno.Status == EnumStatus.NEGOCIO && retorno.Retorno == "Agencia de destino inválida");
        }

        [Fact]
        public void ContaDestinoInvalidaRetornaErroNegocio()
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

            BaseRetorno retorno = transacao.ValidaDeposito();

            Assert.True(retorno.Status == EnumStatus.NEGOCIO && retorno.Retorno == "Conta de destino inválida.");
        }

        [Fact]
        public void ContaDestinoVaziaInvalidaRetornaErroNegocio()
        {

            TransacaoDeposito transacao = new TransacaoDeposito()
            {
                Id = "1d9b4f37-7fac-467f-a1e1-b33ec2e8025f",
                Nome = "Luana Souza",
                AgenciaDestino = "0001",
                NumeroContaDestino = "",
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

            BaseRetorno retorno = transacao.ValidaDeposito();

            Assert.True(retorno.Status == EnumStatus.NEGOCIO && retorno.Retorno == "Conta de destino inválida.");
        }

        [Fact]
        public void ContaOrigemInvalidaRetornaErroNegocio()
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

            BaseRetorno retorno = transacao.ValidaDeposito();

            Assert.True(retorno.Status == EnumStatus.NEGOCIO && retorno.Retorno == "Conta de origem inválida.");
        }

        [Fact]
        public void ContaOrigemVaziaInvalidaRetornaErroNegocio()
        {

            TransacaoDeposito transacao = new TransacaoDeposito()
            {
                Id = "1d9b4f37-7fac-467f-a1e1-b33ec2e8025f",
                Nome = "Luana Souza",
                AgenciaDestino = "0001",
                NumeroContaDestino = "123456",
                AgenciaOrigem = "0867",
                NumeroContaOrigem = "",
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

            BaseRetorno retorno = transacao.ValidaDeposito();

            Assert.True(retorno.Status == EnumStatus.NEGOCIO && retorno.Retorno == "Conta de origem inválida.");
        }

        [Fact]
        public void AgenciaOrigemInvalidaRetornaErroNegocio()
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

            BaseRetorno retorno = transacao.ValidaDeposito();

            Assert.True(retorno.Status == EnumStatus.NEGOCIO && retorno.Retorno == "Agencia de origem inválida");
        }

        [Fact]
        public void AgenciaOrigemVaziaInvalidaRetornaErroNegocio()
        {

            TransacaoDeposito transacao = new TransacaoDeposito()
            {
                Id = "1d9b4f37-7fac-467f-a1e1-b33ec2e8025f",
                Nome = "Luana Souza",
                AgenciaDestino = "0001",
                NumeroContaDestino = "123456",
                AgenciaOrigem = "",
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

            BaseRetorno retorno = transacao.ValidaDeposito();

            Assert.True(retorno.Status == EnumStatus.NEGOCIO && retorno.Retorno == "Agencia de origem inválida");
        }

        [Fact]
        public void ValorNegativoRetornaErroNegocio()
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

            BaseRetorno retorno = transacao.ValidaDeposito();

            Assert.True(retorno.Status == EnumStatus.NEGOCIO && retorno.Retorno == $"O valor {transacao.Valor} não pode ser depositado.");
        }

        [Fact]
        public void ValorZeradoRetornaErroNegocio()
        {

            TransacaoDeposito transacao = new TransacaoDeposito()
            {
                Id = "1d9b4f37-7fac-467f-a1e1-b33ec2e8025f",
                Nome = "Luana Souza",
                AgenciaDestino = "0001",
                NumeroContaDestino = "123456",
                AgenciaOrigem = "0867",
                NumeroContaOrigem = "654321",
                Valor = 0,
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

            BaseRetorno retorno = transacao.ValidaDeposito();

            Assert.True(retorno.Status == EnumStatus.NEGOCIO && retorno.Retorno == $"O valor {transacao.Valor} não pode ser depositado.");
        }
    }
}
