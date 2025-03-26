using Solution.DesktopApp.Views;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class AppShellViewModel
{
    public IAsyncRelayCommand ExitCommand => new AsyncRelayCommand(OnExitAsync);

    public IAsyncRelayCommand AddNewCompetition => new AsyncRelayCommand(OnAddNewCompetitionAsync);
    public IAsyncRelayCommand ListAllCompetitions => new AsyncRelayCommand(OnListAllCompetitionsAsync);
    //public IAsyncRelayCommand AddNewTeam => new AsyncRelayCommand(OnAddNewTeamAsync);
    //public IAsyncRelayCommand ListTeams => new AsyncRelayCommand(OnListAllTeamsAsync);


	private async Task OnExitAsync() => Application.Current.Quit();


    private async Task OnAddNewCompetitionAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(ManageCompetitionView.Name);
	}

    private async Task OnListAllCompetitionsAsync()
    {
        Shell.Current.ClearNavigationStack();
		await Shell.Current.GoToAsync(CompetitionListView.Name);
	}

	//private async Task OnAddNewTeamAsync()
	//{
	//	Shell.Current.ClearNavigationStack();
	//	await Shell.Current.GoToAsync(ManageTeamViewModel.Name);
	//}

	//private async Task OnListAllTeamsAsync()
	//{
	//	Shell.Current.ClearNavigationStack();
	//	await Shell.Current.GoToAsync(ListTeamsViewModel.Name);
	//}
}
