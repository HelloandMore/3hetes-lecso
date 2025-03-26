using Solution.Services.Services;

namespace Solution.DesktopApp.Configurations;

public static class ConfigureDI
{
	public static MauiAppBuilder UseDIConfiguration(this MauiAppBuilder builder)
	{
		builder.Services.AddTransient<MainViewModel>();
		builder.Services.AddTransient<ManageCompetitionViewModel>();
		builder.Services.AddTransient<CompetitionListViewModel>();

        builder.Services.AddTransient<MainView>();
		builder.Services.AddTransient<ManageCompetitionView>();
		builder.Services.AddTransient<CompetitionListView>();

        builder.Services.AddScoped<IGoogleDriveService, GoogleDriveService> ();
		builder.Services.AddScoped<ICompetitionService, CompetitionService>();
		builder.Services.AddScoped<ITeamService, TeamService>();


        return builder;
	}
}
