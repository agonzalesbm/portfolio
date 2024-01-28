namespace postgresql_api.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("students")]
public class Student
{
  [Key]
  [Column("id")]
  public int Id { get; set; }

  [StringLength(50)]
  [Column("name")]
  public string? Name { get; set; }

  [StringLength(50)]
  [Column("fathers_name")]
  public string? FathersName { get; set; }

  [StringLength(50)]
  [Column("mothers_name")]
  public string? MothersName { get; set; }

  [Column("born_date")]
  public DateTime BornDate { get; set; }
}
