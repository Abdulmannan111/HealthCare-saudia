using Application.DTOs;
using Application.IRepositories;
using System.Text.Json;

namespace API.Middlewares
{ 
    public class GlobalClusterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IClusterRepository _clusterRepository;
        public GlobalClusterMiddleware(RequestDelegate next, IClusterRepository clusterRepository)
        {
            _next = next;
            _clusterRepository = clusterRepository;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Equals("/api/get-form-clusters", StringComparison.OrdinalIgnoreCase) &&
                context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {
                string regionId = context.Request.Query["regionId"];
                List<ClusterDto> clusters = new List<ClusterDto>();

                if (!string.IsNullOrEmpty(regionId))
                {
                    clusters = await _clusterRepository.GetClustersByRegionIdAsync(regionId);

                    if (clusters == null || clusters.Count == 0)
                    {
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        await context.Response.WriteAsync("No clusters found for the specified region ID.");
                        return;
                    }
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Region ID must be specified.");
                    return;
                }

                context.Response.ContentType = "application/json";
                var jsonResponse = JsonSerializer.Serialize(new { data = clusters });
                await context.Response.WriteAsync(jsonResponse);
                return; // Short-circuit the pipeline
            }

            await _next(context);
        }

    }
}
