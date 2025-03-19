namespace Solution.Database.Entities;

[Table("Location")]
public class LocationEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }


    [Required]
    public string PublicPlace { get; set; }

    [Required]
    public string Adress { get; set; }

    public virtual ICollection<CityEntity> Cities { get; set; }
}
