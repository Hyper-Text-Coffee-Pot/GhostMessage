using Asp.Versioning;
using Asp.Versioning.Conventions;
using GhostMessage.Api.Extensions;

internal class Program
{
	private static void Main(string[] args)
	{
		var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddCors(options =>
		{
			options.AddPolicy(name: MyAllowSpecificOrigins,
				policy =>
				{
					//policy.WithOrigins("http://example.com",
					//					"http://www.contoso.com");
				});
		});

		// Add services to the container.
		builder.Services.AddControllers();

		// Add API versioning to the container.
		// https://github.com/dotnet/aspnet-api-versioning/tree/main/examples/AspNetCore/WebApi/ByNamespaceExample
		builder.Services.AddApiVersioning(options =>
		{
			// reporting api versions will return the headers
			// "api-supported-versions" and "api-deprecated-versions"
			options.ReportApiVersions = true;
		}).AddMvc(options =>
		{
			// automatically applies an api version based on the name of
			// the defining controller's namespace
			options.Conventions.Add(new VersionByNamespaceConvention());
		});

		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();

		builder.Services.AddSwaggerGen();

		// Register custom dependencies.
		builder.RegisterCustomDependencies();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseCors(MyAllowSpecificOrigins);

		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}