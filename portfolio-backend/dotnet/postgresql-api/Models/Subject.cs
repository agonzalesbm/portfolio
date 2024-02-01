using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace postgresql_api.Models;

[Table("subjects")]
public class Subject
{
  [Key]
  [Column("id")]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }

  [Column("name")]
  public string? Name { get; set; }
}
