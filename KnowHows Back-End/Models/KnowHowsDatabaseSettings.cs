namespace KnowHows_Back_End.Models
{
    public class KnowHowsDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ArticleCollectionName { get; set; } = null!;
    }
}
