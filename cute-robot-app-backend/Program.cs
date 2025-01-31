using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using project_nebula_backend.JWTAuthentication;
using Service;
using Service.Admin;
using Service.BlobStorage;
using Service.Metrics;
using Service.Robot;
using Service.User;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();


builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); ;

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Cors", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

//-------------------------- AZURE STORAGE ------------------------------------------------------
//var storageConnection = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");

//builder.Services.AddAzureClients(azureBuilder => 
//{
//    azureBuilder.AddBlobServiceClient(storageConnection);
//});

//string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
//var blobStorageService = new BlobStorageService(connectionString);

builder.Services.AddScoped<IBlobStorageService>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<BlobStorageService>>();
    //var connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
    var connectionString = builder.Configuration["StorageConnectionString"];

    if (string.IsNullOrEmpty(connectionString))
    {
        logger.LogError("AZURE_STORAGE_CONNECTION_STRING environment variable is not set.");
        throw new Exception("Azure Storage connection string is not set.");
    }

    return new BlobStorageService(connectionString, logger);
});


builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRobotService, RobotService>();
builder.Services.AddScoped<IMetricsService, MetricsService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAdminService, AdminService>();
//builder.Services.AddScoped<IBlobStorageService>(_ => blobStorageService); 

//---------------------------------  JWT-Token  -------------------------------------------------------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
//--------------------------JWT-Token stuff----------------------------------------------------------------------

var app = builder.Build();

app.MapGet("/", () => "You can make API calls to the endpoints in Postman now");

app.UseCors("Cors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
