namespace Solution.Core.Models;

public class LocationModel : IObjectValidator<uint>
{
    public uint Id { get; set; }

    public string PublicPlace { get; set; }

    public string Address { get; set; }

    public LocationModel()
    {
    }

    public LocationModel(uint id, string publicPlace, string address)
    {
        Id = id;
        publicPlace = publicPlace;
        Address = address;
    }

    public LocationModel(LocationEntity entity)
    {
        if (entity is null)
        {
            return;
        }
        Id = entity.Id;
        PublicPlace = entity.PublicPlace;
        Address = entity.Address;
    }

    public override bool Equals(object? obj)
    {
        return obj is LocationModel model &&
            this.Id == model.Id &&
            this.PublicPlace  == model.PublicPlace &&
            this.Address == model.Address;
    }
}
