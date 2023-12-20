# Article Service

The ArticleService contains the methods to connect to the database, this uses the connection string, database name, and collection name to use the database to  request GET, POST, PUT, DELETE commands.

When a new specific action with the database needs to be added, it needs to be added here. The ArticleService has an interface it uses called IArticleService, this is used to make use of Dependency Injection. So when the ArticleService is modified, the IArticleService also needs to be modified to accompany the changes.

<br></br>

The methods of the ArticleService:
- Task<List<Article GetArticlesAsync(): This method will get all the Articles from the database.
- Task<Article? GetArticleAsync(string id): This method will get a specific Article based on the given id. The questionmark means that the Article does not have to exist.
- async Task CreateArticleAsync(Article newPost): This method will create an Article in the database with the provided data.
- async Task UpdateArticleAsync(string id, Article updatedArticle): This method will update an already existing Article with the provided data, the specific Article is found by the provided id.
