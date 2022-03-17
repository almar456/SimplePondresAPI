using SimplePondresAPI;
using Microsoft.EntityFrameworkCore;
using SimplePondresAPI.Models;

//Initialize builder and configure services
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SimplePondresContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Configure app
app.UseSwagger();
app.UseSwaggerUI();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

//Create Get endpoints;
app.MapGet("/", () => Results.Redirect("/swagger/index.html")).ExcludeFromDescription();
app.MapGet("/api/products", async (SimplePondresContext db) => await db.products.ToListAsync());
app.MapGet("/api/products/{id}", async (SimplePondresContext db, int id) => await db.products.FindAsync(id));
app.MapGet("/api/orders", async (SimplePondresContext db) => await db.orders.ToListAsync());
app.MapGet("/api/orders/{id}", async (SimplePondresContext db, int id) => await db.orders.FindAsync(id));

//Create Post endpoints
app.MapPost("/api/orders", async (SimplePondresContext db, Order order) =>
{
    await db.orders.AddAsync(order);
    await db.SaveChangesAsync();
    Results.Accepted();
});

app.MapPost("/internalApi/products", async (SimplePondresContext db, Product product) =>
{
    await db.products.AddAsync(product);
    await db.SaveChangesAsync();
    Results.Accepted();
});

//Create Delete endpoint
app.MapDelete("/internalApi/products/{id}", async (SimplePondresContext db, int id) =>
{
    var product = await db.products.FindAsync(id);
    if (product == null)
    {
        return Results.NotFound();
    } else
    {
        db.products.Remove(product);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
});

//Create Put endpoint
app.MapPut("/internalApi/orders/{id}", async (SimplePondresContext db, int id, string state) =>
{
    var order = await db.orders.FindAsync(id);
    if (order == null)
    {
        return Results.NotFound();
    }
    else
    {
        order.state = state;
        db.Update(order);
        await db.SaveChangesAsync();

        return Results.Ok();
    }
});


app.Run();
