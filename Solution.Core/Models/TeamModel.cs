namespace Solution.Core.Models
{
    public class TeamModel
    {
        public string Id { get; set; }

        public ValidatableObject<string> Name { get; set; }

        public ValidatableObject<CompetitionModel> Competition { get; set; }

        public ValidatableObject<List<MemberModel>> Members { get; set; }

        public TeamModel()
        {
            this.Name = new ValidatableObject<string>();
            this.Competition = new ValidatableObject<CompetitionModel>();
            this.Members = new ValidatableObject<List<MemberModel>>();
            this.Members.Value = new List<MemberModel>();

            AddValidators();
        }

        public TeamModel(TeamEntity entity) : this()
        {
            this.Id = entity.PublicId;
            this.Name.Value = entity.Name;
            this.Competition.Value = new CompetitionModel(entity.Competition);
            this.Members.Value = entity.Members.Select(m => new MemberModel(m)).ToList();
        }

        public TeamEntity ToEntity()
        {
            return new TeamEntity
            {
                PublicId = Id,
                Name = Name.Value,
                CompetitionId = uint.Parse(Competition.Value.Id),
                Members = Members.Value.Select(m => m.ToEntity()).ToList()
            };
        }

        public void ToEntity(ref TeamEntity entity)
        {
            entity.PublicId = Id;
            entity.Name = Name.Value;
            entity.CompetitionId = uint.Parse(Competition.Value.Id);
            entity.Members = Members.Value.Select(m => m.ToEntity()).ToList();
        }

        private void AddValidators()
        {
            this.Name.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Name is required"
            });

            this.Members.Validations.Add(new MaxMembersRule<MemberModel>
            {
                ValidationMessage = "A team can have a maximum of 10 members"
            });
        }
    }
}
