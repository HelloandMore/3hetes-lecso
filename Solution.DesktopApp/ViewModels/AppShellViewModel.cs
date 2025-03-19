namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class AppShellViewModel
{
	public IAsyncRelayCommand ExitCommand => new AsyncRelayCommand(OnExitAsync);
	public IAsyncRelayCommand AddCompetitionCommand => new AsyncRelayCommand(OnAddCompetitionAsync);
	public IAsyncRelayCommand ListAllCompetitionCommand => new AsyncRelayCommand(OnListCompetitionAsync);

	private async Task OnListCompetitionAsync()
	{
		throw new NotImplementedException();
	}

	private async Task OnAddCompetitionAsync()
	{
		Shell.Current.ClearNavigationStack();
		await Shell.Current.GoToAsync(CompetitionManager.Name);
	}

	private async Task OnExitAsync() => Application.Current.Quit();
}
