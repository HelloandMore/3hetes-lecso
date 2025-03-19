
namespace Solution.Database.Entities;

[Table("City")]
public class CityEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }    

    public uint PostalCode { get; set; }

    [Required]
    public string Name { get; set; }

    [ForeignKey("Location")]
    public uint LocationId { get; set; }
    public virtual LocationEntity Location { get; set; }
}
