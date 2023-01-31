
using Microsoft.Extensions.DependencyInjection;
using Processador.Deposito;
using Processador.Deposito.Core.Ports.UseCase;

var serviceCollection = new ServiceCollection();
serviceCollection.RegistraServicos();
IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

var processador = serviceProvider.GetRequiredService<IUSCProcessar>();
try
{
    await processador.Processar();
} catch (Exception e)
{
    Console.WriteLine("=============================================");
    Console.WriteLine(e.Message);
    Console.WriteLine("=============================================\n");
}
// See https://aka.ms/new-console-template for more information

