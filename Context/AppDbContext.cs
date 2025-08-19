using CrudSimple.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudSimple.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Occupation> Occupations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(tb =>
            {
                tb.HasKey(e => e.Id);
                tb.Property(e => e.Id).UseIdentityColumn();
                tb.Property(e => e.FullName).HasMaxLength(50);

                tb.HasOne(e => e.Occupations)       
                  .WithMany(o => o.Employees)      
                  .HasForeignKey(e => e.IdOccupation);

                tb.ToTable("Employee");
            });

            modelBuilder.Entity<Occupation>(tb =>
            {
                tb.HasKey(o => o.Id);
                tb.Property(o => o.Id).UseIdentityColumn();
                tb.Property(o => o.Name).HasMaxLength(50);

                tb.ToTable("Occupation");

                tb.HasData(
                    new Occupation { Id = 1, Name = "Full Stack Dev" },
                    new Occupation { Id = 2, Name = "QA" },
                    new Occupation { Id = 3, Name = "Dev Ops" }
                );
            });

        }
    }
}
