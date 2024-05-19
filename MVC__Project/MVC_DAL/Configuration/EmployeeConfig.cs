


namespace MVC_DAL.Configuration
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employes");
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
            builder.Property(p => p.Address)
    .HasColumnType("varchar")
    .HasMaxLength(255)
    .HasDefaultValue("pp")
    .IsRequired();
            builder.Property(p => p.PhoneNumber)
 .HasColumnType("varchar")
 .HasMaxLength(255)
 .HasDefaultValue("pp")
 .IsRequired();
            builder.Property(p => p.Email)
 .HasColumnType("varchar")
 .HasMaxLength(255)
 .HasDefaultValue("pp")
 .IsRequired();
            builder.Property(p => p.Age)
 .HasColumnType("int")
 .HasDefaultValue(7)
 .IsRequired();
            builder.Property(p => p.Salary)
 .HasColumnType("decimal(18,2)")
 .HasDefaultValue(55)
 .IsRequired();
            builder.Property(p => p.IsActive)
 .HasDefaultValue(false)
 .IsRequired();
            builder.Property(p => p.HireDate)
 .HasColumnType("Date")
 .HasDefaultValue("2000-8-9")
 .IsRequired();
            builder.HasOne(p => p.Department)
                .WithMany(p => p.Employees)
                .HasForeignKey(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
