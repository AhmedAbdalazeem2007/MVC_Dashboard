


namespace MVC_DAL.Contexts
{
    public class MVCDataBase:IdentityDbContext<ApplicationUser>
    {
        public MVCDataBase(DbContextOptions<MVCDataBase> options):base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
    }
}
