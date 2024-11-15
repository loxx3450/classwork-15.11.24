using classwork.Core;
using classwork.Core.Interfaces;
using classwork.Core.Models;
using classwork.Storage;
using Microsoft.EntityFrameworkCore;

namespace classwork_15._11._24.Services
{
    public class ArticleService : IArticleService
    {
        private readonly DataContext _dataContext;

        public ArticleService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Article>> GetArticles(ArticleFilter filter, int skip = 0, int take = 20, CancellationToken cancellationToken = default)
        {
            var articles = _dataContext.Articles
                .Include(a => a.Source)
                .AsQueryable();

            if (filter.Region is not null)
            {
                articles = articles.Where(a => a.Region.Equals(filter.Region));
            }
            if (filter.Topic is not null)
            {
                articles = articles.Where(a => a.Topic.Equals(filter.Topic));
            }
            if (filter.From is not null)
            {
                articles = articles.Where(a => a.PublicationDate >= filter.From);
            }
            if (filter.Until is not null)
            {
                articles = articles.Where(a => a.PublicationDate <= filter.Until);
            }

            return await articles
                .Skip(skip)
                .Take(take)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<Article?> GetArticleById(int id, CancellationToken cancellationToken = default)
        {
            return await _dataContext.Articles
                .Include(a => a.Source)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<Article> AddArticle(Article article, CancellationToken cancellationToken = default)
        {
            _dataContext.Articles.Add(article);

            await _dataContext.SaveChangesAsync(cancellationToken);

            return article;
        }

        public async Task<Article> UpdateArticle(int id, Article article, CancellationToken cancellationToken = default)
        {
            var existingArticle = await _dataContext.Articles.FindAsync(keyValues: [id], cancellationToken: cancellationToken);

            if (existingArticle is null)
            {
                throw new KeyNotFoundException();
            }

            existingArticle.Title = article.Title;
            existingArticle.PublicationDate = article.PublicationDate;
            existingArticle.SourceId = article.SourceId;
            existingArticle.Region = article.Region;
            existingArticle.Topic = article.Topic;

            await _dataContext.SaveChangesAsync(cancellationToken);

            return existingArticle;
        }

        public async Task DeleteArticle(int id, CancellationToken cancellationToken = default)
        {
            var article = await _dataContext.Articles.FindAsync(keyValues: [id], cancellationToken: cancellationToken);

            if (article is not null)
            {
                _dataContext.Articles.Remove(article);
                await _dataContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
