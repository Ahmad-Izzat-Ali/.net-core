using InvoiceApi.Data;
using InvoiceApi.Dtos;
using InvoiceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApi.Endpoints;


public static class SupplierEndpoint
{
     const string GetSupplierEndpointName = "GetSupplier";

    public static RouteGroupBuilder MapSuppliersEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("suppliers");

        // GET /suppliers
        group.MapGet("/", async (AppDbContext dbContext) =>
            await dbContext.Suppliers
                .AsNoTracking()
                .ToListAsync());

        // GET /suppliers/1
        group.MapGet("/{id:int}", async (int id, AppDbContext dbContext) =>
        {
            var supplier = await dbContext.Suppliers.FindAsync(id);
            return supplier is null 
                ? Results.NotFound() 
                : Results.Ok(supplier); // Optionally, map to DTO here
        })
        .WithName(GetSupplierEndpointName);

        // POST /suppliers
        group.MapPost("/", async (CreateSupplierDto newSupplier, AppDbContext dbContext) =>
        {
            var supplier = new Supplier
            {
                ContactName = newSupplier.ContactName,
                Address = newSupplier.Address,
                City = newSupplier.City,
                PostalCode = newSupplier.PostalCode,
                Country = newSupplier.Country,
                Phone = newSupplier.Phone,
                Email = newSupplier.Email,
                Website = newSupplier.Website
            };

            dbContext.Suppliers.Add(supplier);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(
                GetSupplierEndpointName,
                new { id = supplier.Id },
                supplier);
        });

        // PUT /suppliers/1
        group.MapPut("/{id:int}", async (int id, UpdateSupplierDto updatedSupplier, AppDbContext dbContext) =>
        {
            var existingSupplier = await dbContext.Suppliers.FindAsync(id);
            if (existingSupplier is null)
            {
                return Results.NotFound();
            }

            existingSupplier.ContactName = updatedSupplier.ContactName;
            existingSupplier.Address = updatedSupplier.Address;
            existingSupplier.City = updatedSupplier.City;
            existingSupplier.PostalCode = updatedSupplier.PostalCode;
            existingSupplier.Country = updatedSupplier.Country;
            existingSupplier.Phone = updatedSupplier.Phone;
            existingSupplier.Email = updatedSupplier.Email;
            existingSupplier.Website = updatedSupplier.Website;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE /suppliers/1
        group.MapDelete("/{id:int}", async (int id, AppDbContext dbContext) =>
        {
            var supplier = await dbContext.Suppliers.FindAsync(id);
            if (supplier is null)
            {
                return Results.NotFound();
            }

            dbContext.Suppliers.Remove(supplier);
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        return group;
    }
}