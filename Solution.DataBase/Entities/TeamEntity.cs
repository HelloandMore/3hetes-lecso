namespace Solution.Database.Entities;
[Table("Team")]
public class TeamEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    public string PublicId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [ForeignKey("Competition")]
    public uint CompetitionId { get; set; }
    public virtual CompetitionEntity Competition { get; set; }

    public virtual ICollection<MemberEntity> Members { get; set; } = new List<MemberEntity>();
}
