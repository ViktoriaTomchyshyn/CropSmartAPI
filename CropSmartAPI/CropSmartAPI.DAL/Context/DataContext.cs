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
        bool useSql = Environment.GetEnvironmentVariable("USE_SQL_SERVER") == "true";
        if (!optionsBuilder.IsConfigured) // Ensure this is not already configured from elsewhere
        {
            if (useSql)
            {
                optionsBuilder.UseSqlServer(
                    @"Data Source=DESKTOP-JJTRH2D;Initial Catalog=CropSmartDB;Integrated Security=True");
            }
            else
            {
                optionsBuilder.UseSqlite("Data Source=cropsmartDB.db");
            }
        }
    }
    //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-JJTRH2D;Initial Catalog=CropSmartDB;Integrated Security=True");


    public async Task CompleteAsync()
    {
        await SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}