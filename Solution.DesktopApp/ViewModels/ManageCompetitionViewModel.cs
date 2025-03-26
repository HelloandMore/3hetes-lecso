using Microsoft.EntityFrameworkCore;
using Solution.Core.Models;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class ManageCompetitionViewModel(AppDbContext dbContext, ICompetitionService competitionService) : CompetitionModel()
{
	#region life cycle commands
	public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingkAsync);
	public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
	public IAsyncRelayCommand ImageSelectCommand => new AsyncRelayCommand(OnImageSelectAsync);
	public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);

	#endregion

	#region Validation commands
	public IRelayCommand LocationIndexChangedCommand => new RelayCommand(() => this.Location.Validate());
	public IRelayCommand DateIndexChangedCommand => new RelayCommand(() => this.Date.Validate());
	public IRelayCommand NameValidationCommand => new RelayCommand(() => this.Name.Validate());

	#endregion

	[ObservableProperty]
	private string title;

	[ObservableProperty]
	private List<LocationModel> locations = [];

	[ObservableProperty]
	private ImageSource image;

	private FileResult selectedFile = null;

	private delegate Task ButtonActionDelagate();
	private ButtonActionDelagate asyncButtonAction;

	public async void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		await Task.Run(LoadLocationsAsync);

		bool hasValue = query.TryGetValue("Competition", out object result);

		if (!hasValue)
		{
			asyncButtonAction = OnSaveAsync;
			Title = "Add new competition";
			return;
		}

		CompetitionModel competition = result as CompetitionModel;

		this.Id = competition.Id;
		this.Name.Value = competition.Name.Value;
		this.Date.Value = competition.Date.Value;
		this.Location.Value = competition.Location.Value;
		//this.ImageId = competition.ImageId;
		//this.WebContentLink = competition.WebContentLink;

		//if (!string.IsNullOrEmpty(competition.WebContentLink))
		//{
		//	Image = new UriImageSource
		//	{
		//		Uri = new Uri(competition.WebContentLink),
		//		CacheValidity = new TimeSpan(10, 0, 0, 0)
		//	};
		//}

		asyncButtonAction = OnUpdateAsync;
		Title = "Update competition";
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

		var result = await competitionService.UpdateAsync(this);

		var message = result.IsError ? result.FirstError.Description : "Competition updated.";
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

		var result = await competitionService.CreateAsync(this);
		var message = result.IsError ? result.FirstError.Description : "Competition saved.";
		var title = result.IsError ? "Error" : "Information";

		if (!result.IsError)
		{
			ClearForm();
		}

		await Application.Current.MainPage.DisplayAlert(title, message, "OK");
	}

	private async Task OnSubmitAsync() => await asyncButtonAction();

	private async Task LoadLocationsAsync()
	{
		Locations = await dbContext.Locations.AsNoTracking()
											 .OrderBy(x => x.PublicPlace)
											 .Select(x => new LocationModel(x))
											 .ToListAsync();
	}

	private void ClearForm()
	{
		this.Date.Value = null;
		this.Location.Value = null;
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
