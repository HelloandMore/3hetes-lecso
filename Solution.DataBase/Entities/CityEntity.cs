
namespace Solution.Database.Entities;

[Table("City")]
[Index(nameof(Name), IsUnique = true)]
public class CityEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }
    [Required]
    public uint PostalCode { get; set; }

    [Required]
    public string Name { get; set; }

}
