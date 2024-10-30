using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApi.Models;

using System;
using System.ComponentModel.DataAnnotations;

public class Invoice
{
    [Key]
    public int Id { get; set; } // Assuming you want an Id field for the primary key

    [ForeignKey("Supplier")]
    public int SupplierId { get; set; } // Foreign key to Supplier

    [Required]
    [StringLength(50)]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public virtual Supplier Supplier { get; set; } = null!;
}