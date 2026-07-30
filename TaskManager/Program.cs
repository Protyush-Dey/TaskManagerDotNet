using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text;
using TaskManager.Services;
using TaskManager.Utils;
using TodoList.Config;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
DotNetEnv.Env.Load();
builder.Configuration.AddEnvironmentVariables();
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<
        Microsoft.Extensions.Options.IOptions<DatabaseSettings>>().Value);

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
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter JWT Token like: Bearer {your token}"
    });


    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
}); builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TokenGenerator>();
//builder.Services.AddScoped<UserService>();
builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    Environment.GetEnvironmentVariable("JWT_ISSUER"),

                ValidAudience =
                    Environment.GetEnvironmentVariable("JWT_AUDIENCE"),

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            Environment.GetEnvironmentVariable("JWT_KEY")!
                        )
                    ),

                RoleClaimType =
                    System.Security.Claims.ClaimTypes.Role
            };
    });


builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();

app.Run();
