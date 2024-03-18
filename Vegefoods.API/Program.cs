using Serilog;
using Vegefoods.API.Extensions;
using Vegefoods.Application.Extensions;
using Vegefoods.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.		
//var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
//var dbName = Environment.GetEnvironmentVariable("DB_NAME");
//var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
builder.Services.AddApplicationLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(policy =>
{
	policy.AddPolicy("CorsPolicy", opt => opt
		.AllowAnyOrigin()
		.AllowAnyHeader()
		.AllowAnyMethod());
});

var _loggrer = new LoggerConfiguration()
.ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext()
.CreateLogger();
builder.Logging.AddSerilog(_loggrer);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
	app.UseSwagger();
	app.UseSwaggerUI();
//}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseErrorHandler();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();

public partial class Program { }