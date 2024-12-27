using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Drive.Data.Entities.Models;
using File = Drive.Data.Entities.Models.File;

/*using Drive.Data.Seeds;*/
 
namespace Drive.Data.Entities;

public class DriveDbContext : DbContext
{
    public DriveDbContext(DbContextOptions<DriveDbContext> options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server= 127.0.0.1;Port=5432;Database=Users;User Id=postgres;Pasword=4aE8tGEC;");
    }
    public DbSet<User> Users { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<Folder> Folders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id); 
        
        modelBuilder.Entity<File>()
            .HasKey(f => f.Id);
        
        modelBuilder.Entity<Folder>()
            .HasKey(f => f.Id);
        
        modelBuilder.Entity<User>()
            .Property(u => u.Mail)
            .IsRequired() 
            .HasMaxLength(100); 
        
        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .IsRequired() 
            .HasMaxLength(100); 
        
        modelBuilder.Entity<Folder>()
            .Property(f => f.FolderName)
            .IsRequired()
            .HasMaxLength(150);
        
        modelBuilder.Entity<File>()
            .Property(f => f.FileName)
            .IsRequired()
            .HasMaxLength(150);
        
        modelBuilder.Entity<File>()
            .Property(f => f.EditingTime)
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Id)
            .IsUnique(); 
        
        modelBuilder.Entity<Folder>()
            .HasIndex(f => f.Id)
            .IsUnique();
        
        modelBuilder.Entity<File>()
            .HasIndex(f => f.Id)
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