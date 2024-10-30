using System.ComponentModel.DataAnnotations;

namespace InvoiceApi.Dtos;

public record class UpdateSupplierDto(
    [Required][StringLength(100)] string ContactName,
    [Required][StringLength(250)] string Address,
    [Required][StringLength(100)] string City,
    [Required][StringLength(20)] string PostalCode,
    [Required][StringLength(100)] string Country,
    [Required][Phone] string Phone,
    [EmailAddress] string? Email,
    [Url] string? Website
);