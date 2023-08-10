using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Serializable]
public partial class Lead
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string PINCode { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string MobileNumber { get; set; }

    public string EmailAddress { get; set; }

    [ForeignKey(nameof(PINCode))]
    public virtual Subarea Subarea { get; set; } = null!;
}