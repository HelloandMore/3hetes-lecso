using Microsoft.EntityFrameworkCore;
using Solution.Core.Models;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class ManageTeamViewModel(AppDbContext dbContext, ITeamService teamService) : TeamModel()
{
	#region life cycle commands
	public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingkAsync);
	public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
	public IAsyncRelayCommand ImageSelectCommand => new AsyncRelayCommand(OnImageSelectAsync);
	public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);

	#endregion

	#region Validation commands
	public IRelayCommand PointsValidationCommand => new RelayCommand(() => this.Points.Validate());
	public IRelayCommand NameValidationCommand => new RelayCommand(() => this.Name.Validate());

	#endregion

	[ObservableProperty]
	private string title;

	[ObservableProperty]
	private IList<LocationModel> locations = [];

	[ObservableProperty]
	private IList<TeamModel> teams = [];

	[ObservableProperty]
	private IList<CompetitionModel> competitions = [];

	[ObservableProperty]
	private ImageSource image;

	private FileResult selectedFile = null;

	private delegate Task ButtonActionDelagate();
	private ButtonActionDelagate asyncButtonAction;

	public async void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		await Task.Run(LoadLocationsAsync);

		bool hasValue = query.TryGetValue("Team", out object result);

		if (!hasValue)
		{
			asyncButtonAction = OnSaveAsync;
			Title = "Add new team";
			return;
		}

		TeamModel team = result as TeamModel;

		this.Id = team.Id;
		this.Name.Value = team.Name.Value;
		this.
		//this.ImageId = team.ImageId;
		//this.WebContentLink = team.WebContentLink;

		//if (!string.IsNullOrEmpty(team.WebContentLink))
		//{
		//	Image = new UriImageSource
		//	{
		//		Uri = new Uri(team.WebContentLink),
		//		CacheValidity = new TimeSpan(10, 0, 0, 0)
		//	};
		//}

		asyncButtonAction = OnUpdateAsync;
		Title = "Update team";
	}

	private async Task OnImageSelectAsync()
	{

	}

	private async Task OnAppearingkAsync()
	{
	}

	private async Task OnDisappearingAsync()
	{
	}

	private async Task OnUpdateAsync()
	{
		if (!IsFormValid())
		{
			return;
		}

		//await UploaImageAsync();

		var result = await teamService.UpdateAsync(this);

		var message = result.IsError ? result.FirstError.Description : "Team updated.";
		var title = result.IsError ? "Error" : "Information";

		await Application.Current.MainPage.DisplayAlert(title, message, "OK");
	}

	private async Task OnSaveAsync()
	{
		if (!IsFormValid())
		{
			return;
		}

		//await UploadImageAsync();

		var result = await teamService.CreateAsync(this);
		var message = result.IsError ? result.FirstError.Description : "Team saved.";
		var title = result.IsError ? "Error" : "Information";

		if (!result.IsError)
		{
			ClearForm();
		}

		await Application.Current.MainPage.DisplayAlert(title, message, "OK");
	}

	private async Task OnSubmitAsync() => await asyncButtonAction();

	private async Task LoadCompetitions()
	{
		Competitions = await dbContext.Competitions.AsNoTracking()
											 .OrderBy(x => x.Id)
											 .Select(x => new CompetitionModel(x))
											 .ToListAsync();
	}

	private void ClearForm()
	{
		
		this.Name.Value = null;
	}

	private bool IsFormValid()
	{
		this.Date.Validate();
		this.Location.Validate();
		this.Name.Validate();

		return this.Date.IsValid && this.Location.IsValid && this.Name.IsValid;
	}
}
