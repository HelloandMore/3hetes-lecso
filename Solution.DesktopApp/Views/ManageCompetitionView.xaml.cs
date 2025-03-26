namespace Solution.DesktopApp.Views;

public partial class ManageCompetitionView : ContentPage
{
	public ManageCompetitionViewModel ViewModel => this.BindingContext as ManageCompetitionViewModel;

	public static string Name => nameof(ManageCompetitionView);

	public ManageCompetitionView(ManageCompetitionViewModel viewModel)
	{
		this.BindingContext = viewModel;

		InitializeComponent();
	}
}