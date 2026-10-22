using Microsoft.Extensions.Logging;
using GoalTracker.Services;
using GoalTracker.ViewModels;
#if MAUI_DEVFLOW
using Microsoft.Maui.DevFlow.Agent;
#endif

namespace Group9Project;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
#if MAUI_DEVFLOW
		builder.AddMauiDevFlowAgent();
#endif
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<GoalViewModel>();
		builder.Services.AddSingleton<GoalDatabaseService>();
		builder.Services.AddSingleton<AppShell>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
