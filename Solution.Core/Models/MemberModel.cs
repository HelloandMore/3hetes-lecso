namespace Solution.Core.Models
{
    public class MemberModel
    {
        public string Id { get; set; }

        public ValidatableObject<string> Name { get; set; }

        public ValidatableObject<TeamModel> Team { get; set; }

        public MemberModel()
        {
            this.Name = new ValidatableObject<string>();
            this.Team = new ValidatableObject<TeamModel>();

            AddValidators();
        }

        public MemberModel(MemberEntity entity) : this()
        {
            this.Id = entity.PublicId;
            this.Name.Value = entity.Name;
            this.Team.Value = new TeamModel(entity.Team);
        }

        public MemberEntity ToEntity()
        {
            return new MemberEntity
            {
                PublicId = Id,
                Name = Name.Value,
                TeamId = uint.Parse(Team.Value.Id)
            };
        }

        public void ToEntity(ref MemberEntity entity)
        {
            entity.PublicId = Id;
            entity.Name = Name.Value;
            entity.TeamId = uint.Parse(Team.Value.Id);
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
