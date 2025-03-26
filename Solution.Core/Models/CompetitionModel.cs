namespace Solution.Core.Models
{
    public partial class CompetitionModel
    {
        public string Id { get; set; }

        public ValidatableObject<string> Name { get; set; }

        public ValidatableObject<uint?> Date { get; set; }

        public ValidatableObject<LocationModel> Location { get; set; }

        public CompetitionModel()
        {
            this.Name = new ValidatableObject<string>();
            this.Date = new ValidatableObject<uint?>();
            this.Location = new ValidatableObject<LocationModel>();

            AddValidators();
        }

        public CompetitionModel(CompetitionEntity entity) : this()
        {
            this.Id = entity.PublicId;
            this.Name.Value = entity.Name;
            this.Date.Value= entity.Date;
            this.Location.Value = new LocationModel(entity.Location);
        }

        public CompetitionEntity ToEntity()
        {
            return new CompetitionEntity
            {
                PublicId = Id,
                Name = Name.Value,
                Date = Date.Value ?? 0,
                LocationId = Location.Value.Id
            };
        }

        public void ToEntity(CompetitionEntity entity)
        {
            entity.PublicId = Id;
            entity.LocationId = Location.Value.Id;
            entity.Name = Name.Value;
            entity.Date = Date.Value ?? 0;
        }

        private void AddValidators()
        {
            this.Location.Validations.Add(new PickerValidationRule<LocationModel>
            {
                ValidationMessage = "Location is required"
            });

            this.Name.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Name is required"
            });

            this.Date.Validations.AddRange(
            new List<IValidationRule<uint?>>
            {
                new IsNotNullOrEmptyRule<uint?>
                {
                    ValidationMessage = "Date must be selected"
                },
                new MaxValueRule<uint?>(DateTime.Now.Day)
                {
                    ValidationMessage = "Competition must be on a day before today."
                }
            });
        }

    }
}
