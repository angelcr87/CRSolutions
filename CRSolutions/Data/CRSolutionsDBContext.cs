using CRSolutions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CRSolutions.Data
{
    public class CRSolutionsDBContext : DbContext
    {
        public CRSolutionsDBContext(DbContextOptions<CRSolutionsDBContext> options)
            : base(options)
        {
        }


        public DbSet<Company> Companies { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Candidate> Candidates { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                       .HasOne(u => u.Role)
                       .WithMany(r => r.Users)
                       .HasForeignKey(u => u.IdRol);

            modelBuilder.Entity<User>()
                       .HasOne(u => u.Company)
                       .WithMany(c => c.Users)
                       .HasForeignKey(u => u.IdCompany);

            modelBuilder.Entity<Candidate>()
                       .HasOne(c => c.User)
                       .WithMany(u => u.Candidates)
                       .HasForeignKey(c => c.IdUser);

            modelBuilder.Entity<Candidate>()
                       .HasOne(c => c.Company)
                       .WithMany(u => u.Candidates)
                       .HasForeignKey(c => c.IdCompany);
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var configuration = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json")
        //        .Build();

        //    var connectionString = configuration.GetConnectionString("CRSolutionsDBContext");
        //    optionsBuilder.UseSqlServer(connectionString);
        //}
    }
}
