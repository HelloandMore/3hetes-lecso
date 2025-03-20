namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class ManageCompetitionViewModel (AppDbContext dbContext) : CompetitionModel()
{
	#region life cycle commands
	public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingkAsync);
	public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
	#endregion

	#region Validation commands
	public IRelayCommand LocationIndexChangedCommand => new RelayCommand(() => this.Location.Validate());
	public IRelayCommand DateIndexChangedCommand => new RelayCommand(() => this.Date.Validate());
	// További validációs parancsok majd ide
	public IRelayCommand CityValidationCommand => new RelayCommand(() => this.City.Validate());
	public IRelayCommand PostalCodeValidationCommand => new RelayCommand(() => this.PostalCode.Validate());
	public IRelayCommand NameValidationCommand => new RelayCommand(() => this.Name.Validate());
	public IRelayCommand AddressValidationCommand => new RelayCommand(() => this.Address.Validate());
	public IRelayCommand PhoneNumberValidationCommand => new RelayCommand(() => this.PhoneNumber.Validate());

	#endregion

	private async Task OnAppearingkAsync()
	{
	}

	private async Task OnDisappearingAsync()
	{ 
	}
}
