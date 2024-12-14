using Microsoft.EntityFrameworkCore;
using Namespace.Data;
using Namespace.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProductsDb"));

var app = builder.Build();

app.MapProductEndpoints();

app.Run();
