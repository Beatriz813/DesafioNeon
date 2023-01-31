using APIRest.Deposito.Core.Enums;
using APIRest.Deposito.Core.Ports.Adapters.Mongo;
using Microsoft.AspNetCore.Mvc;

namespace APIRest.Deposito.Endpoint
{
    public static class RotasExtension
    {
        public static void MapRotas(this IEndpointRouteBuilder app)
        {
            app.MapGet("/transacoes", async (IServiceProvider provider) =>
            {
                var logRepository = provider.GetRequiredService<ILogTransacaoRepository>();
                var retorno = await logRepository.RecuperaTodosLogs();
                return Results.Ok(retorno);
            });

            app.MapGet("/transacoes/status/{status}", async ([FromRoute] int status, IServiceProvider provider) =>
            {
                var logRepository = provider.GetRequiredService<ILogTransacaoRepository>();
                var retorno = await logRepository.RecuperaLogsPorStatus((EnumStatus) status);
                return Results.Ok(retorno);
            });

            app.MapGet("/transacoes/idTransacao/{idTransacao}", async ([FromRoute] string idTransacao, IServiceProvider provider) =>
            {
                var logRepository = provider.GetRequiredService<ILogTransacaoRepository>();
                var retorno = await logRepository.RecuperaLogsPorId(idTransacao);
                return Results.Ok(retorno);
            });
        }
    }
}
