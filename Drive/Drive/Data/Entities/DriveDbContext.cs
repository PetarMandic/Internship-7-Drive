using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Drive.Data.Entities.Models;
using Drive.Data.Seeds;
using File = Drive.Data.Entities.Models.File;

namespace Drive.Data.Entities;

public class DriveDbContext : DbContext
{
    public DriveDbContext(DbContextOptions<DriveDbContext> options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server= 127.0.0.1;Port=5432;Database=Users;User Id=postgres;Password=4aE8tGEC;");
    }
    public DbSet<User> Users { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<Folder> Folders { get; set; }
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
    
    modelBuilder.Entity<Folder>()
        .HasKey(f => f.Id);

    modelBuilder.Entity<Folder>()
        .Property(f => f.Name)
        .IsRequired()
        .HasMaxLength(150);

    
    modelBuilder.Entity<Folder>()
        .HasOne(f => f.User)               
        .WithMany(u => u.Folders)          
        .HasForeignKey(f => f.UserId)      
        .OnDelete(DeleteBehavior.Cascade);
    
    modelBuilder.Entity<Folder>()
        .HasOne(f => f.ParentFolder)      
        .WithMany(f => f.SubFolders)     
        .HasForeignKey(f => f.ParentFolderId)  
        .OnDelete(DeleteBehavior.SetNull);   

    modelBuilder.Entity<File>()
        .HasKey(f => f.Id);

    modelBuilder.Entity<File>()
        .Property(f => f.Name)
        .IsRequired()
        .HasMaxLength(150);

    modelBuilder.Entity<File>()
        .Property(f => f.EditingTime)
        .IsRequired();

    modelBuilder.Entity<File>()
        .Property(f => f.Text)
        .HasColumnType("text[]")
        .IsRequired();
    
    modelBuilder.Entity<File>()
        .HasOne(f => f.Folder)
        .WithMany(fl => fl.Files)
        .HasForeignKey(f => f.FolderId) 
        .OnDelete(DeleteBehavior.SetNull); 

    DatabaseSeeder.Seed(modelBuilder);
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