using System.Net.Http.Json;
using BlazorWebAppOidc.Models.Articles;
using BlazorWebAppOidc.Services.Interfaces;

namespace BlazorWebAppOidc.Services.Http
{
    public class ArticleService : IArticleService
    {
        private readonly HttpClient _httpClient;
        private readonly string ArticlesUrl = "/api/Articles";

        public ArticleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ArticleShortDto>> Get()
        {
            var url = $"{ArticlesUrl}";
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<ArticleShortDto>>(url);
            }
            catch (Exception ex)
            {
                return new List<ArticleShortDto> { new ArticleShortDto { DescriptionN = ex.Message } };
            }
        }
    }
}
