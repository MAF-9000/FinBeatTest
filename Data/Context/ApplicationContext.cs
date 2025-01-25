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
            // Конфигурация для таблицы codeValue
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

            // Конфигурация для таблицы apiLogs
            modelBuilder.Entity<ApiLog>(entity =>
            {
                entity.ToTable("apiLogs");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Path)
                    .HasMaxLength(255)
                    .HasColumnName("path");

                entity.Property(e => e.QueryString)
                    .IsRequired(false)
                    .HasMaxLength(255)
                    .HasColumnName("queryString");

                entity.Property(e => e.Method)
                    .HasMaxLength(255)
                    .HasColumnName("method");

                entity.Property(e => e.Response)
                    .HasMaxLength(255)
                    .HasColumnName("response");

                entity.Property(e => e.Payload)
                    .IsRequired(false)
                    .HasMaxLength(255)
                    .HasColumnName("payload");

                entity.Property(e => e.ResponseCode)
                    .HasColumnName("responseCode");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
