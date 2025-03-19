namespace Solution.Database.Entities;
[Table("Competition")]
public class CompetitionEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [StringLength(168)]
    [Required]
    public string PublicId { get; set; }

    [Required]
    [StringLength(168)]
    public string Name { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [ForeignKey("Location")]
    public uint LocationId { get; set; }
    public virtual LocationEntity Location { get; set; }

    public virtual ICollection<TeamEntity> Teams { get; set; }
    public virtual ICollection<RefEntity> Refs { get; set; }
}
