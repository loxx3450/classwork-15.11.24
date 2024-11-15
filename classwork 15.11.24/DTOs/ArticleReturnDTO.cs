using classwork.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace classwork_15._11._24.DTOs
{
    public class ArticleReturnDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public virtual Source Source { get; set; }
        public string Region { get; set; }
        public string Topic { get; set; }
    }
}
