
using Microsoft.Extensions.DependencyInjection;
using Processador.Deposito.Adapters.Email;
using Processador.Deposito.Adapters.HttpCliente;
using Processador.Deposito.Adapters.Mongo.Repository;
using Processador.Deposito.Core.Ports.Adapters.Email;
using Processador.Deposito.Core.Ports.Adapters.HttpCliente;
using Processador.Deposito.Core.Ports.Adapters.Mongo.Repository;
using Processador.Deposito.Core.Ports.UseCase;
using Processador.Deposito.Core.Ports.UseCase.Processar;

var serviceCollection = new ServiceCollection();
serviceCollection.AddScoped<ILoteDepositoHttpCliente, LoteDepositoHttpCliente>();
serviceCollection.AddScoped<IContasHttpCliente, ContasHttpCliente>();
serviceCollection.AddScoped<IEmailCliente, EmailCliente>();
serviceCollection.AddScoped<IUSCProcessar, USCProcessar>();
serviceCollection.AddScoped<ILogRepository, LogRepository>();
IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

var processador = serviceProvider.GetRequiredService<IUSCProcessar>();
await processador.Processar();
// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
