using InvoiceApi.Data;
using InvoiceApi.Dtos;
using InvoiceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApi.Endpoints;

public static class InvoicesEndpoints
{
    const string GetInvoiceEndpointName = "GetInvoice";

    public static RouteGroupBuilder MapInvoicesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("invoices"); // Removed .WithParameterValidation()

        // GET /invoices
        group.MapGet("/", async (AppDbContext dbContext) =>
            await dbContext.Invoices
                .Include(invoice => invoice.Supplier) // Include related supplier
                .AsNoTracking()
                .ToListAsync());

        // GET /invoices/{id}
        group.MapGet("/{id}", async (int id, AppDbContext dbContext) =>
        {
            var invoice = await dbContext.Invoices
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(i => i.Id == id);

            return invoice is null ? Results.NotFound() : Results.Ok(invoice);
        })
        .WithName(GetInvoiceEndpointName);

        // POST /invoices
        group.MapPost("/", async (CreateInvoiceDto newInvoice, AppDbContext dbContext) =>
        {
            var invoice = new Invoice
            {
                SupplierId = newInvoice.SupplierId,
                InvoiceNumber = newInvoice.InvoiceNumber,
                Status = newInvoice.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            dbContext.Invoices.Add(invoice);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetInvoiceEndpointName, new { id = invoice.Id }, invoice);
        });

        // PUT /invoices/{id}
        group.MapPut("/{id}", async (int id, UpdateInvoiceDto updatedInvoice, AppDbContext dbContext) =>
        {
            var existingInvoice = await dbContext.Invoices.FindAsync(id);
            if (existingInvoice is null)
            {
                return Results.NotFound();
            }

            existingInvoice.SupplierId = updatedInvoice.SupplierId;
            existingInvoice.InvoiceNumber = updatedInvoice.InvoiceNumber;
            existingInvoice.Status = updatedInvoice.Status;
            existingInvoice.UpdatedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        // DELETE /invoices/{id}
        group.MapDelete("/{id}", async (int id, AppDbContext dbContext) =>
        {
            var existingInvoice = await dbContext.Invoices.FindAsync(id);
            if (existingInvoice is null)
            {
                return Results.NotFound();
            }

            dbContext.Invoices.Remove(existingInvoice);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        return group;
    }
}