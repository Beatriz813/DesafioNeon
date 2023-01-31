using APIRest.Deposito.Core.ObjetosValor;

namespace APIRest.Deposito.Endpoint
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        private ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                BaseRetorno obj = new BaseRetorno("Erro de sistema.", Core.Enums.EnumStatus.SISTEMA);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync<BaseRetorno>(obj);

            }
        }

    }

    public static class ExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }

}
