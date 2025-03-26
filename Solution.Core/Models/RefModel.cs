using Microsoft.Identity.Client;
using Microsoft.Maui.Platform;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Solution.Core.Models
{
    public partial class RefModel
    {
        public string Id { get; set; }

        public ValidatableObject<string> Name { get; set; }

        public ValidatableObject<string> PhoneNumber { get; set; }

        public ValidatableObject<string> Email { get; set; }

        public ValidatableObject<CompetitionModel> Competition { get; set; }

        public RefModel()
        {
            this.Name = new ValidatableObject<string>();
            this.PhoneNumber = new ValidatableObject<string>();
            this.Email = new ValidatableObject<string>();
            this.Competition = new ValidatableObject<CompetitionModel>();

            AddValidators();
        }

        public RefModel(RefEntity entity) : this()
        {
            this.Id = entity.PublicId;
            this.Name.Value = entity.Name;
            this.PhoneNumber.Value = entity.PhoneNumber;
            this.Email.Value = entity.Email;
            this.Competition.Value = new CompetitionModel(entity.Competition);
        }

        public RefEntity ToEntity()
        {
            return new RefEntity
            {
                PublicId = Id,
                Name = Name.Value,
                PhoneNumber = PhoneNumber.Value,
                Email = Email.Value,
                CompetitionId = uint.Parse(Competition.Value.Id)
            };
        }

        public void ToEntity(ref RefEntity entity)
        {
            entity.PublicId = Id;
            entity.Name = Name.Value;
            entity.PhoneNumber = PhoneNumber.Value;
            entity.Email = Email.Value;
            entity.CompetitionId = uint.Parse(Competition.Value.Id);
        }


        private void AddValidators()
        {
            this.Name.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Name is required"
            });

            this.PhoneNumber.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Phone number is required"
            });

            this.Email.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Email is required"
            });
        }
    }
}
