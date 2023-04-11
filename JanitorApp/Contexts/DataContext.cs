

using JanitorApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JanitorApp.Context;

internal class DataContext : DbContext
{
    private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\C Sharp\JanitorApp\JanitorApp\JanitorApp\Contexts\sql_db.mdf"";Integrated Security=True;Connect Timeout=30";

    #region Constructors
    public DataContext()
    {
    }
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    #endregion

    #region Overrides
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(_connectionString);
    }
    #endregion

    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<CaseEntity> Cases { get; set; } = null!;
    public DbSet<StatusEntity> Statuses { get; set; } = null!;
}
