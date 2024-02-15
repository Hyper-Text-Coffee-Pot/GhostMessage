namespace GhostMessage.Api.Extensions;

public static class DependencyExtensions
{
	public static void RegisterCustomDependencies(this WebApplicationBuilder builder)
	{
		builder.Services.AddTransient<ICryptographyService, CryptographyService>();
	}
}