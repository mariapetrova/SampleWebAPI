using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Serializable]
public partial class Person
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string PINCode { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string MobileNumber { get; set; }

    public string EmailAddress { get; set; }

    public decimal Salary { get; set; }

    public int DepartmentId { get; set; }

    [ForeignKey(nameof(DepartmentId))]
    public virtual Department Department { get; set; } = null!;
}