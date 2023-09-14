namespace KnowHows_Back_End.Models
{
    public class KnowHowsDatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string PostCollectionName { get; set; } = string.Empty;
    }
}
