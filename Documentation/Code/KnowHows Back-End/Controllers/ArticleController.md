The ArticleController contains the endpoints where de CRUD actions can be activated for the Articles.

The standard HTTP localhost URL is: http://localhost:5201/
The standard HTTPS localhost URL is: https://localhost:7119/

After the standard URL can be placed endpoint extensions to call on a specific endpoint to use the CRUD actions.
The endpoints are:

| Endpoint | Action |
| ---------------- | --------------- |
| {GET}/api/Article | Get all Articles |
| {POST}/api/Article | Create a new Article with the given data |
| {PUT}/api/Article/updateLikes/id | Update the LikesScore for the specified Article |

When a endpoint is being called on, it will trigger to act and do the action it was said to do.