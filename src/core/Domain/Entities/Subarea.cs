using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public partial class Subarea
{
    [Key]
    public string PINCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Lead> Leads { get; } = new List<Lead>();
}
