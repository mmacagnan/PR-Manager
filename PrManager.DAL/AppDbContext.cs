using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PrManager.BL.Models;

namespace PrManager.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Congregation> Congregations { get; set; }
        public DbSet<Publicator> Publicators { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(x => x.UserName).IsUnique();
            modelBuilder.Entity<Congregation>().HasIndex(x => new
            {
                x.CongregationName,
                x.CongregationNumber
            }).IsUnique();
            modelBuilder.Entity<Publicator>().HasIndex(x => new
            {
                x.FirstName,
                x.LastName
            }).IsUnique();
            modelBuilder.Entity<UserRole>().HasIndex(x => new
            {
                x.RoleId,
                x.UserId
            }).IsUnique();
        }
    }
    
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext> 
    { 
        public AppDbContext CreateDbContext(string[] args) 
        { 
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../PrManager.UI/appsettings.json").Build(); 
            var builder = new DbContextOptionsBuilder<AppDbContext>(); 
            var connectionString = configuration.GetConnectionString("DefaultConnection"); 
            builder.UseSqlServer(connectionString); 
            return new AppDbContext(builder.Options); 
        } 
    }
}