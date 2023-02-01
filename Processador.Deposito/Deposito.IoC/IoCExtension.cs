using Microsoft.Extensions.DependencyInjection;
using Processador.Deposito.Adapters.Email;
using Processador.Deposito.Adapters.HttpCliente;
using Processador.Deposito.Adapters.Mongo.Repository;
using Processador.Deposito.Core.Ports.Adapters.Email;
using Processador.Deposito.Core.Ports.Adapters.HttpCliente;
using Processador.Deposito.Core.Ports.Adapters.Mongo.Repository;
using Processador.Deposito.Core.Ports.UseCase;
using Processador.Deposito.Core.Ports.UseCase.Processar;

namespace Processador.Deposito
{
    public static class IoCExtension
    {
        public static void RegistraServicos(this IServiceCollection services)
        {
            services.AddScoped<ILoteDepositoHttpCliente, LoteDepositoHttpCliente>();
            services.AddScoped<IContasHttpCliente, ContasHttpCliente>();
            services.AddScoped<IEmailCliente, EmailCliente>();
            services.AddScoped<IUSCProcessar, USCProcessar>();
            services.AddScoped<ILogRepository, LogRepository>();
        }
    }
}
