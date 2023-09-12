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

            //Prepararmos los datos iniciales a pregargar

            var Role = new Role()
            {
                IdRol = Guid.Parse("4A74DA66-BCD1-4662-8625-CB7C3BF2A837"),
                RoleName = "Admin",
                Description = "Puede crear nuevos candidatos",
                Status = true
            };

            var Role2 = new Role()
            {
                IdRol = Guid.Parse("789A3411-ED70-42BD-9681-B0D9AE800583"),
                RoleName = "Cliente Admin",
                Description = "Puede todos los candidatos",
                Status = true
            };

            var Role3 = new Role()
            {
                IdRol = Guid.Parse("882E1047-29E1-4276-8BCE-6F2372670AE1"),
                RoleName = "Cliente",
                Description = "puede ver unicamente sus candidatos",
                Status = true
            };
            modelBuilder.Entity<Role>().HasData(new Role[] { Role, Role2, Role3 });

            var Company = new Company()
            {
                IdCompany = Guid.Parse("127CFEF3-3E5D-4B47-B58D-19ADB61CF6BE"),
                CompanyName = "CRSolutions",
                CompanyDescription = "Compania Inicial",
                BusinessName = "razon Social",
                RFC = "RFC",
                Credits = true,
                Status = true
            };

            modelBuilder.Entity<Company>().HasData(Company);


            var User = new User()
            {
                IdUser = Guid.NewGuid(),
                FullName = "Administrador",
                UserName = "Admin",
                Password = "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", //12345
                Status = true,
                IdRol = Guid.Parse("4A74DA66-BCD1-4662-8625-CB7C3BF2A837"),
                IdCompany = Guid.Parse("127CFEF3-3E5D-4B47-B58D-19ADB61CF6BE")
            };

            var User2 = new User()
            {
                IdUser = Guid.NewGuid(),
                FullName = "Cliente Administrador",
                UserName = "Cliente_Admin",
                Password = "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", //12345
                Status = true,
                IdRol = Guid.Parse("789A3411-ED70-42BD-9681-B0D9AE800583"),
                IdCompany = Guid.Parse("127CFEF3-3E5D-4B47-B58D-19ADB61CF6BE")
            };

            var User3 = new User()
            {
                IdUser = Guid.NewGuid(),
                FullName = "Cliente",
                UserName = "Cliente",
                Password = "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", //12345
                Status = true,
                IdRol = Guid.Parse("882E1047-29E1-4276-8BCE-6F2372670AE1"),
                IdCompany = Guid.Parse("127CFEF3-3E5D-4B47-B58D-19ADB61CF6BE")
            };

            modelBuilder.Entity<User>().HasData(new User[] { User, User2, User3 });

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
