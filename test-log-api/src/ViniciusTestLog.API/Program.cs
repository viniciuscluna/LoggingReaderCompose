using ViniciusTestLog.API.Models;
using ViniciusTestLog.API.Repositories;
using ViniciusTestLog.API.Services;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<LoggingDatabaseSettings>(
    builder.Configuration.GetSection("LoggingDatabase"));

builder.Services.AddSingleton<IMongoRepository, MongoRepository>();
builder.Services.AddSingleton<ILogPersistorService, LogPersistorService>();
builder.Services.AddSingleton<ILogQueryService, LogQueryService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyHeader();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();


app.Run();
