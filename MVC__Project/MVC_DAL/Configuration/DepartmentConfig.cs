

namespace MVC_DAL.Configuration
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
             .ValueGeneratedOnAdd()
             .IsRequired()
             .UseIdentityColumn(1, 1);
            builder.Property(p => p.Name)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .HasDefaultValue("pp")
                .IsRequired();
            builder.Property(p => p.Code)
    .HasColumnType("varchar")
    .HasMaxLength(255)
    .HasDefaultValue("pp")
    .IsRequired();
        }
    }
}
