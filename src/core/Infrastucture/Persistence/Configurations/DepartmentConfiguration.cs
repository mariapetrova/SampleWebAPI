namespace Infrastucture.Persistence.Configurations;
public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");

        builder
            .HasKey(e => e.Id)
            .HasName("PK_Department");

        builder
            .Property(e => e.Name)
            .HasMaxLength(125)
            .IsRequired()
            .IsUnicode(false);
    }
}