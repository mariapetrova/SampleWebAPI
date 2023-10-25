namespace Infrastucture.Persistence.Configurations;
public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Persons");

        builder
            .HasKey(e => e.Id)
            .HasName("PK_Person");

        builder
            .Property(e => e.Name)
            .HasMaxLength(125)
            .IsRequired()
            .IsUnicode(false);

        builder
           .Property(e => e.PINCode)
           .HasMaxLength(25)
           .IsRequired()
           .IsUnicode(false);

        builder
            .Property(e => e.Address)
            .HasMaxLength(500)
            .IsRequired()
            .IsUnicode(false);

        builder
            .Property(e => e.MobileNumber)
            .HasMaxLength(25);

        builder
            .Property(e => e.EmailAddress)
            .HasMaxLength(100);

        builder
           .Property(e => e.Salary);

        builder
           .Property(e => e.DepartmentId)
           .IsRequired();

        builder
            .HasOne(d => d.Department)
            .WithMany(p => p.Persons)
            .HasForeignKey(d => d.DepartmentId);
    }
}