using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;

namespace close_and_cheap.Data
{
    public class AllowedCorsMiddleware
    {
        private readonly RequestDelegate _next;
        public AllowedCorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //Fill according your roles
            context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "*" });


            context.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "*" });


            await _next(context);

        }
    }
}
