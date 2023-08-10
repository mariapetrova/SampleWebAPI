namespace Infrastucture.Persistence.Configurations;
public class LeadConfiguration : IEntityTypeConfiguration<Lead>
{
    public void Configure(EntityTypeBuilder<Lead> builder)
    {
        builder.ToTable("Leads");

        builder
            .HasKey(e => e.Id)
            .HasName("PK_Leads");

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
            .HasOne(d => d.Subarea)
            .WithMany(p => p.Leads)
            .HasForeignKey(d => d.PINCode);
    }
}