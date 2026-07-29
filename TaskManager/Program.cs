using MongoDB.Bson;
using MongoDB.Driver;
using TodoList.Config;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
DotNetEnv.Env.Load();
builder.Configuration.AddEnvironmentVariables();
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDB"));


var mongoSettings = builder.Configuration
    .GetSection("MongoDB")
    .Get<DatabaseSettings>();

try
{
    var client = new MongoClient(mongoSettings!.CollectionString);

    var database = client.GetDatabase(mongoSettings.DataBaseName);

    database.RunCommand<BsonDocument>("{ping:1}");

    Console.WriteLine("MongoDB Connected Successfully");
}
catch (Exception ex)
{
    Console.WriteLine($"? MongoDB Connection Failed: {ex.Message}");
}

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

app.UseAuthorization();

app.MapControllers();

app.Run();
