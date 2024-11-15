using classwork.Core.Interfaces;
using classwork.Core.Models;
using classwork.Storage;
using Microsoft.EntityFrameworkCore;

namespace classwork_15._11._24.Services
{
    public class SourceService : ISourceService
    {
        private readonly DataContext _dataContext;

        public SourceService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Source>> GetSources(int skip = 0, int take = 10, CancellationToken cancellationToken = default)
        {
            return await _dataContext.Sources
                .Skip(skip)
                .Take(take)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<Source?> GetSourceById(int id, CancellationToken cancellationToken = default)
        {
            var source = await _dataContext.Sources.FindAsync(keyValues: [id], cancellationToken: cancellationToken);

            return source;
        }

        public async Task<Source> AddSource(Source source, CancellationToken cancellationToken = default)
        {
            _dataContext.Add(source);

            await _dataContext.SaveChangesAsync(cancellationToken);

            return source;
        }
    }
}
