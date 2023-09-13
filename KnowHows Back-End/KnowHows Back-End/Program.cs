using MongoDB.Bson;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

const string connectionUri = "mongodb+srv://435518:<password>@knowhowscluster.iwmreky.mongodb.net/?retryWrites=true&w=majority";
var settings = MongoClientSettings.FromConnectionString(connectionUri);
// Set the ServerApi field of the settings object to Stable API version 1
settings.ServerApi = new ServerApi(ServerApiVersion.V1);
// Create a new client and connect to the server
var client = new MongoClient(settings);
// Send a ping to confirm a successful connection
try
{
    var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
    System.Diagnostics.Debug.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
}
catch (Exception ex)
{
    System.Diagnostics.Debug.WriteLine(ex);
    System.Diagnostics.Debug.WriteLine("You fucking suck");
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
