namespace Solution.Database.Entities;
[Table("Ref")]
public class RefEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public uint Id { get; set; }

    public string PublicId { get; set; }

    [Required]
    [StringLength(168)]
    public string Name { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public string Email { get; set; }


    [ForeignKey("Competition")]
    public uint CompetitionId { get; set; }
    public virtual CompetitionEntity Competition { get; set; }
}
