using BlazorWebAppOidc.Models.Articles;

namespace BlazorWebAppOidc.Services.Interfaces
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleShortDto>> Get();
    }
}
