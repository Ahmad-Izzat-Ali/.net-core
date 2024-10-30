using InvoiceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApi.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Define DbSets for your entities
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Invoice> Invoices { get; set; } = null!;

}