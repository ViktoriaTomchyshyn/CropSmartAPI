using CropSmartAPI.DAL.Entities;
using Microsoft.Data.SqlClient;
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

        if (!optionsBuilder.IsConfigured) // Ensure this is not already configured from elsewhere
        {
            // Check if SQL Server connection can be established
            try
            {
                SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-JJTRH2D;Initial Catalog=CropSmartDB;Integrated Security=True");
                sqlConnection.Open();
                sqlConnection.Close();

                // If SQL Server connection is successful, use SQL Server
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-JJTRH2D;Initial Catalog=CropSmartDB;Integrated Security=True");
            }
            catch (Exception)
            {
                // If SQL Server connection fails, use SQLite
                optionsBuilder.UseSqlite("Data Source=cropsmartDB.db");
            }
        }
        //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-JJTRH2D;Initial Catalog=CropSmartDB;Integrated Security=True");
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
