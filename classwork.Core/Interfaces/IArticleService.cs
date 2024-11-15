using classwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classwork.Core.Interfaces
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetArticles(ArticleFilter filter, int skip = 0, int take = 20, CancellationToken cancellationToken = default);

        Task<Article?> GetArticleById(int id, CancellationToken cancellationToken = default);

        Task<Article> AddArticle(Article article, CancellationToken cancellationToken = default);

        Task<Article> UpdateArticle(int id, Article article, CancellationToken cancellationToken = default);

        Task DeleteArticle(int id, CancellationToken cancellationToken = default);
    }
}
