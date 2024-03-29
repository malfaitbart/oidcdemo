using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Headers;

namespace BlazorWebAppOidc
{
    public static class HttpClientExtensions
    {
        public static IHttpClientBuilder AddAuthToken(this IHttpClientBuilder builder)
        {
            builder.Services.TryAddTransient<HttpClientAuthorizationDelegatingHandler>();

            builder.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            return builder;
        }

        private class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor, HttpMessageHandler innerHandler) : base(innerHandler)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if (_httpContextAccessor.HttpContext is HttpContext context)
                {
                    var accessToken = await context.GetTokenAsync("access_token");

                    if (accessToken is not null)
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    }
                }

                var response = await base.SendAsync(request, cancellationToken);

                return response;
            }
        }
    }
}
