using Microsoft.EntityFrameworkCore;
using Namespace.Models;
using Namespace.Data;

namespace Namespace.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this WebApplication app)
        {
            app.MapGet("/api/products", async (AppDbContext db) =>
                await db.Products.ToListAsync());

            app.MapGet("/api/products/{id}", async (int id, AppDbContext db) =>
                await db.Products.FindAsync(id) is Product product
                    ? Results.Ok(product)
                    : Results.NotFound());

            app.MapPost("/api/products", async (Product product, AppDbContext db) =>
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();

                return Results.Created($"/api/products/{product.Id}", product);
            });

            app.MapPut("/api/products/{id}", async (int id, Product updatedProduct, AppDbContext db) =>
            {
                var product = await db.Products.FindAsync(id);

                if (product is null) return Results.NotFound();

                product.Name = updatedProduct.Name;
                product.Price = updatedProduct.Price;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            app.MapDelete("/api/products/{id}", async (int id, AppDbContext db) =>
            {
                var product = await db.Products.FindAsync(id);

                if (product is null) return Results.NotFound();

                db.Products.Remove(product);
                await db.SaveChangesAsync();

                return Results.NoContent();
            });
        }
    }
}
