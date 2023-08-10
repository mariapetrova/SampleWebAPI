namespace Infrastucture.Persistence.Configurations;
public class SubareaConfiguration : IEntityTypeConfiguration<Subarea>
{
    public void Configure(EntityTypeBuilder<Subarea> builder)
    {
        builder.ToTable("Subareas");

        builder
            .HasKey(e => e.PINCode)
            .HasName("PK_Subareas");

        builder
            .Property(e => e.Name)
            .HasMaxLength(125)
            .IsRequired()
            .IsUnicode(false);
    }
}