namespace classwork.Core
{
    public class ArticleFilter
    {
        public string? Topic { get; set; } = null;
        public string? Region { get; set; } = null;
        public DateTime? From { get; set; } = null;
        public DateTime? Until { get; set; } = null;
    }
}
