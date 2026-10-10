using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Application.IRepositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace API.Middlewares
{
    public class JwtClaimsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAccessTokens _accessTokensRepository;
        private readonly IMemoryCache _cache;

        public JwtClaimsMiddleware(RequestDelegate next, IAccessTokens accessTokensRepository, IMemoryCache cache)
        {
            _next = next;
            _accessTokensRepository = accessTokensRepository;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Skip for PlatformController routes
                var path = context.Request.Path.Value?.ToLower();

                if (path != null && path.StartsWith("/api/platform"))
                {
                    await _next(context); // Skip middleware logic
                    return;
                }

                if (!ValidateCustomHeader(context))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("{\"error\": \"Bot attack detected\"}");
                    return;
                }

                var token = ExtractToken(context);
                if (string.IsNullOrEmpty(token))
                {
                    await RespondWithUnauthorized(context);
                    return;
                }

                var publicKey = context.RequestServices.GetRequiredService<IConfiguration>()?["JwtSettings:SecretKey"];
                if (string.IsNullOrEmpty(publicKey))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("{\"error\": \"The secret key did not match.\"}");
                    return;
                }

                try
                {
                    var claimsPrincipal = ValidateToken(token, publicKey);
                    if (claimsPrincipal == null)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("{\"error\": \"The secret key did not match.\"}");
                        return;
                    }
                    try
                    {
                        if (!await IsTokenRevoked(claimsPrincipal))
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            await context.Response.WriteAsync("{\"error\": \"Token is expired.\"}");
                            return;
                        }
                    }
                    catch
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("{\"error\": \"DB is not connected\"}");
                        return;
                    }

                    StoreClaimsInContext(context, claimsPrincipal);
                }
                catch
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("{\"error\": \"The secret key did not match.\"}");
                    return;
                }

                await _next(context);
            }
            catch
            {

                await RespondWithUnauthorized(context);
                return;
            }
        }

        #region private methods
        private bool ValidateCustomHeader(HttpContext context)
        {
            var customHeader = context.Request.Headers["X-Custom-Header"].ToString();
            if (string.IsNullOrEmpty(customHeader) || _cache.TryGetValue(customHeader, out _))
                return false;

            _cache.Set(customHeader, customHeader, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(3)
            });

            return true;
        }
        private static string? ExtractToken(HttpContext context)
        {
            return context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        }

        private static async Task RespondWithUnauthorized(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid token");
        }

        private static ClaimsPrincipal? ValidateToken(string token, string publicKey)
        {
            var rsa = RSA.Create();
            rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(publicKey), out _);

            var handler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new RsaSecurityKey(rsa),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            };

            return handler.ValidateToken(token, validationParameters, out _);
        }

        private async Task<bool> IsTokenRevoked(ClaimsPrincipal claimsPrincipal)
        {
            var tokenId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;
            return tokenId != null && await _accessTokensRepository.CheckRevokeAsync(tokenId);
        }

        private static void StoreClaimsInContext(HttpContext context, ClaimsPrincipal claimsPrincipal)
        {
            context.Items["UserId"] = claimsPrincipal.Claims.FirstOrDefault(c =>
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            context.Items["ClientId"] = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "aud")?.Value;
            context.Items["TokenId"] = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;

            var expiry = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;
            if (expiry != null && double.TryParse(expiry, out double expiryTimestamp))
            {
                context.Items["ExpiryDate"] = DateTimeOffset.FromUnixTimeSeconds((long)expiryTimestamp).UtcDateTime;
            }
        }
        #endregion
    }
}
