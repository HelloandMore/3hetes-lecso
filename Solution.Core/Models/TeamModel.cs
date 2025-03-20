namespace Solution.Core.Models
{
    public class TeamModel
    {
        public string Id { get; set; }

        public ValidatableObject<string> Name { get; set; }

        public ValidatableObject<CompetitionModel> Competition { get; set; }

        public TeamModel()
        {
            this.Name = new ValidatableObject<string>();
            this.Competition = new ValidatableObject<CompetitionModel>();

            AddValidators();
        }

        public TeamModel(TeamEntity entity) : this()
        {
            this.Id = entity.PublicId;
            this.Name.Value = entity.Name;
            this.Competition.Value = new CompetitionModel(entity.Competition);
        }

        public TeamEntity ToEntity()
        {
            return new TeamEntity
            {
                PublicId = Id,
                Name = Name.Value,
                CompetitionId = uint.Parse(Competition.Value.Id)
            };
        }

        public void ToEntity(ref TeamEntity entity)
        {
            entity.PublicId = Id;
            entity.Name = Name.Value;
            entity.CompetitionId = uint.Parse(Competition.Value.Id);
        }


        private void AddValidators()
        {
            this.Name.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Name is required"
            });
        }
    }
}
