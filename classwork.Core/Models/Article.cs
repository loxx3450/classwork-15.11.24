using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classwork.Core.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public virtual Source Source { get; set; }

        [ForeignKey(nameof(Source))]
        public int SourceId { get; set; }

        public string Region { get; set; }
        public string Topic { get; set; }
    }
}
