using Microsoft.EntityFrameworkCore;

namespace CRSolutions.Models
{
    public class CRSolutionsDBContext : DbContext
    {
        public CRSolutionsDBContext(DbContextOptions<CRSolutionsDBContext> opciones)
            : base(opciones) {       
        }

        
        public DbSet<Company> Companies { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Candidate> Candidates { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("CRSolutionsDBContext");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
