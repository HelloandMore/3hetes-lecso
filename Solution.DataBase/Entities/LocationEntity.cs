namespace Solution.Database.Entities;

[Table("Location")]
public class LocationEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [Required]
    public uint PostalCode { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string PublicPlace { get; set; }

    [Required]
    public string Adress { get; set; }

}
