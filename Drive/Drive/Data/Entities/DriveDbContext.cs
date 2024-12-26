using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Drive.Data.Entities.Models;
using Drive.Data.Seeds;
 
namespace Drive.Data.Entities;

public class DriveDbContext : DbContext
{
    public DriveDbContext(DbContextOptions<DriveDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);  
        
        modelBuilder.Entity<User>()
            .Property(u => u.Mail)
            .IsRequired() 
            .HasMaxLength(100); 
        
        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .IsRequired() 
            .HasMaxLength(100); 
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Id)
            .IsUnique(); 
        
        base.OnModelCreating(modelBuilder);
    }

    public class DriveDbContextFactory : IDesignTimeDbContextFactory<DriveDbContext>
    {
        public DriveDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("App.config")
                .Build();

            config.Providers
                .First()
                .TryGet("connectionStrings:add:Drive:connectionString", out var connectionString);

            var options = new DbContextOptionsBuilder<DriveDbContext>()
                .UseNpgsql(connectionString)
                .Options;

            return new DriveDbContext(options);
        }
    }
}