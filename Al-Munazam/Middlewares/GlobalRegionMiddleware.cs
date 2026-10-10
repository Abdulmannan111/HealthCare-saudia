using Application.IRepositories;
using System.Text.Json;
using Application.DTOs;

namespace API.Middlewares
{
    public class GlobalRegionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRegionRepository _regionRepository;

        public GlobalRegionMiddleware(RequestDelegate next, IRegionRepository regionRepository)
        {
            _next = next;
            _regionRepository = regionRepository;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if ((context.Request.Path.Equals("/api/get-form-regions", StringComparison.OrdinalIgnoreCase) ||
                 context.Request.Path.Equals("/api/get-all-regions", StringComparison.OrdinalIgnoreCase)) &&
                context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {
                string method = context.Request.Query["method"];
                List<RegionDto> regions;

                if (!string.IsNullOrEmpty(method))
                {
                    regions = await _regionRepository.GetRegionsByMethodAsync(method);
                    if (regions.Count == 0)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync("Invalid method specified.");
                        return;
                    }
                }
                else
                {
                    regions = await _regionRepository.GetRegions();
                }

                context.Response.ContentType = "application/json";
                var jsonResponse = JsonSerializer.Serialize(new { data = regions });
                await context.Response.WriteAsync(jsonResponse);
                return; // Short-circuit the pipeline
            }

            await _next(context); 
        }

    }
}
