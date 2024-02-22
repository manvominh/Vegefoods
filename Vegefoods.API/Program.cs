using Vegefoods.API.Extensions;
using Vegefoods.Application.Extensions;
using Vegefoods.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseErrorHandler();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
