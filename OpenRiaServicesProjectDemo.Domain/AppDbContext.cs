using System.Data.Entity;
using OpenRiaServicesProjectDemo.Domain.Mappings;

namespace OpenRiaServicesProjectDemo.Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("name=App")
        {
        }

        public DbSet<AppTable> AppTables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AppTableMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}