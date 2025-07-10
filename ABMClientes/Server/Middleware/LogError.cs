using ABMClientes.Server.Datos;
using ABMClientes.Shared;
using CineAPI.Datos.ADO.NET;

namespace ABMClientes.Server.Middleware
{
    public class LogError
    {
        private readonly RequestDelegate _next;

        public LogError(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<LogError> logger, IConexion db)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ocurrió un error no manejado.");

                var log = new LogErrores
                {
                    LOG_FECHA = DateTime.Now,
                    LOG_RUTA = context.Request.Path,
                    LOG_MSG = ex.Message
                };

                await db.PostError(log);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Error interno del servidor.");
            }
        }
    }
}
