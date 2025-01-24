using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class LogContext : DbContext
    {
        public DbSet<ApiLog> ApiLogs { get; set; }

        public LogContext(DbContextOptions<LogContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<ApiLog>();
            base.OnModelCreating(modelBuilder);
        }
    }

}