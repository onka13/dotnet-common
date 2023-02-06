using DotNetCommon.Data.EntityFrameworkBase.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCommon.Data.EntityFrameworkBase.Components;

/// <summary>
/// A simple Db Context
/// </summary>
public class InformationDbContext : EmptyDbContext
{
    public virtual DbSet<InformationSchemaColumn> InformationSchemaColumns { get; set; }

    public virtual DbSet<InformationSchemaTable> InformationSchemaTables { get; set; }

    public static new InformationDbContext Init(string provider, string connectionString)
    {
        var context = new InformationDbContext();
        context.Provider = provider;
        context.ConnectionString = connectionString;
        return context;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<InformationSchemaTable>().HasNoKey();
        modelBuilder.Entity<InformationSchemaColumn>().HasNoKey();
    }
}
