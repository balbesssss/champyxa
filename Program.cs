using api.db.Repo;
using api.DB.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/", async (ProductRepository db) => await db.GetProductsAsync());
app.MapPost("/p",
 async (Products body, ProductRepository db ) =>
{
   await db.CreateProduct(body);
   return Results.Created("/",body);
});

app.Run();

