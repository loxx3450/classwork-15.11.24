using classwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classwork.Core.Interfaces
{
    public interface ISourceService
    {
        Task<IEnumerable<Source>> GetSources(int skip, int take, CancellationToken cancellationToken = default);
        Task<Source?> GetSourceById(int id, CancellationToken cancellationToken = default);

        Task<Source> AddSource(Source source, CancellationToken cancellationToken = default);
    }
}
