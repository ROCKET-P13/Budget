using Server.Data;
using Microsoft.EntityFrameworkCore;
using Server.Repositories.BudgetRepository;
using Server.Data.Interfaces;
using Server.Repositories.BudgetRepository.Interfaces;
using Server.Factories.BudgetFactory.Interfaces;
using Server.Factories.BudgetFactory;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDatabaseContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
builder.Services.AddScoped<IBudgetFactory, BudgetFactory>();

builder.Services.AddControllers()
	.AddJsonOptions(options => 
	{
		options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
	});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	policy.AllowAnyOrigin()
		.AllowAnyHeader()
		.AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();


app.Run();