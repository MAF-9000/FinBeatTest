using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<DataRecord> DataRecords { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataRecord>(entity =>
            {
                entity.ToTable("codeValue");

                entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

                entity.Property(e => e.Code)
                .HasColumnName("code");

                entity.Property(e => e.Value)
                .HasMaxLength(255)
                .HasColumnName("value");
            });

            modelBuilder.Entity<DataRecord>()
            .HasIndex(c => c.Value)
            .HasDatabaseName("Value_Index");

            base.OnModelCreating(modelBuilder);
        }
    }
}
