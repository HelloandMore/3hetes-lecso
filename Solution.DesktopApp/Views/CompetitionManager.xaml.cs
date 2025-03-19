namespace Solution.DesktopApp.Views;

public partial class CompetitionManagerViewModel : ContentPage
{
	public CompetitionManagerViewModel ViewModel => this.BindingContext as CompetitionManagerViewModel;

	public static string Name => nameof(CompetitionManagerViewModel);

	public CompetitionManagerViewModel(CompetitionManagerViewModel viewModel)
	{
		this.BindingContext = viewModel;

		InitializeComponent();
	}
}