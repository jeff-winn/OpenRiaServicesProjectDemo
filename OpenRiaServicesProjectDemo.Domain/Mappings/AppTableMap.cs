using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OpenRiaServicesProjectDemo.Domain.Mappings
{
    public class AppTableMap : EntityTypeConfiguration<AppTable>
    {
        public AppTableMap()
        {
            ToTable("AppTables", "dbo");
            HasKey(o => o.Id);

            Property(o => o.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(o => o.Name)
                .HasColumnName("Name")
                .IsRequired();

            Property(o => o.IsEnabled)
                .HasColumnName("IsEnabled");
        }
    }
}