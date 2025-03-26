namespace Solution.Core.Models;

public class CityModel : IObjectValidator<uint>
{
    public uint Id { get; set; }

    public uint PostalCode { get; set; }

    public string Name { get; set; }

    public CityModel()
    {
    }

    public CityModel(uint id, uint postalCode, string name)
    {
        Id = id;
        PostalCode = postalCode;
        Name = name;
    }

    public CityModel(CityEntity entity)
    {
        if (entity is null)
        {
            return;
        }
        Id = entity.Id;
        Name = entity.Name;
        PostalCode = entity.PostalCode;
    }

    public override bool Equals(object? obj)
    {
        return obj is CityModel model &&
            this.Id == model.Id &&
            this.Name == model.Name &&
            this.PostalCode == model.PostalCode;
    }
}
