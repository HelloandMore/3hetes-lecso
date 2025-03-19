using CommunityToolkit.Mvvm.ComponentModel;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class CompetitionManager(AppDbContext dbContext, ICompetitionService competitionService)
{
	#region life cycle commands
	public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingkAsync);
	public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);

	private async Task OnAppearingkAsync()
	{
		
	}
	private async Task OnDisappearingAsync()
	{
		
	}
	#endregion

	#region Validation commands
	#endregion
}
