using System.Text.Json;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.EntityFrameworkCore;
using BudgetAPI.Data;
using BudgetAPI.Data.Interfaces;
using BudgetAPI.Factories.BudgetFactory;
using BudgetAPI.Factories.BudgetFactory.Interfaces;
using BudgetAPI.Factories.BudgetViewModelFactory;
using BudgetAPI.Factories.BudgetViewModelFactory.Interfaces;
using BudgetAPI.Factories.CategoryFactory;
using BudgetAPI.Factories.CategoryFactory.Interfaces;
using BudgetAPI.Finders.BudgetFinder;
using BudgetAPI.Finders.BudgetFinder.Interfaces;
using BudgetAPI.Finders.CategoryFinder;
using BudgetAPI.Finders.CategoryFinder.Interfaces;
using BudgetAPI.Repositories.BudgetRepository;
using BudgetAPI.Repositories.BudgetRepository.Interfaces;
using BudgetAPI.Repositories.CategoryRepository;
using BudgetAPI.Repositories.CategoryRepository.Interfaces;

namespace BudgetAPI;

public class Startup(IConfiguration configuration)
{
	public IConfiguration Configuration { get; } = configuration;

	public void ConfigureServices(IServiceCollection services)
	{
		
		var secretArn = Configuration["POSTGRES_SECRET_ARN"];

		string connectionString = "";

		if (!string.IsNullOrEmpty(secretArn))
		{
			var client = new AmazonSecretsManagerClient();
			var secretValue = client.GetSecretValueAsync(new GetSecretValueRequest
			{
				SecretId = secretArn
			}).Result;

			var secretJson = JsonDocument.Parse(secretValue.SecretString);
			var username = secretJson.RootElement.GetProperty("username").GetString();
			var password = secretJson.RootElement.GetProperty("password").GetString();

			var host = Configuration["DB_HOST"];
			var databaseName = Configuration["DB_NAME"];
			var port = Configuration["DB_PORT"];
		
			connectionString = $"Host={host};Port={port};Username={username};Password={password};Database={databaseName};Pooling=true";
		} else {
			connectionString = Configuration.GetConnectionString("Postgres") ?? "";
		}

		services.AddDbContext<AppDatabaseContext>(options =>
			options.UseNpgsql(connectionString));

		services.AddScoped<IEventStore, EventStore>();

		services.AddScoped<IBudgetRepository, BudgetRepository>();
		services.AddScoped<IBudgetFactory, BudgetFactory>();
		services.AddScoped<IBudgetViewModelFactory, BudgetViewModelFactory>();

		services.AddScoped<ICategoryRepository, CategoryRepository>();
		services.AddScoped<ICategoryFactory, CategoryFactory>();

		services.AddScoped<ICategoryFinder, CategoryFinder>();
		services.AddScoped<IBudgetFinder, BudgetFinder>();

		services.AddControllers()
		.AddJsonOptions(options =>
		{
			options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
		});
	}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		using (var scope = app.ApplicationServices.CreateScope())
		{
			var db = scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
			db.Database.Migrate();
		}

		app.UseHttpsRedirection();
		app.UseRouting();
		app.UseAuthorization();
		
		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
			endpoints.MapGet("/", async context =>
			{
				await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
			});
		});
	}
}