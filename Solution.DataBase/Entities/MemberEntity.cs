﻿namespace Solution.Database.Entities;

[Table("Member")]
public class MemberEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    public string PublicId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [ForeignKey("Team")]
    public uint TeamId { get; set; }
    public virtual TeamEntity Team { get; set; }
}
