using System.ComponentModel.DataAnnotations;

namespace InvoiceApi.Dtos;

public record class UpdateInvoiceDto(
    [Required] int SupplierId,
    [Required][StringLength(50)] string InvoiceNumber,
    [Required][StringLength(20)] string Status
);