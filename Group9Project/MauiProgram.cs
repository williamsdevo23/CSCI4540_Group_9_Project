using Microsoft.Extensions.Logging;
using GoalTracker.Services;
using GoalTracker.ViewModels;

namespace Group9Project;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<GoalViewModel>();
		builder.Services.AddTransient<GoalDatabaseService>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
