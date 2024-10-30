namespace InvoiceApi.Models;

using System;
using System.ComponentModel.DataAnnotations;

public class Supplier
{
    public int Id { get; set; }

    public required string ContactName { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public required string Website { get; set; }

}
