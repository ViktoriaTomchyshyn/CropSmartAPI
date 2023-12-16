using CropSmartAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace CropSmartAPI.DAL.Context;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Field> Fields { get; set; }
    public DbSet<Crop> Crops { get; set; }
    public DbSet<Fertilizer> Fertilizers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-JJTRH2D;Initial Catalog=CropSmartDB;Integrated Security=True");
    }
    public async Task CompleteAsync()
    {
        await SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
