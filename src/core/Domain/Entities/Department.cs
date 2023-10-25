using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public partial class Department
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Person> Persons { get; } = new List<Person>();
}
