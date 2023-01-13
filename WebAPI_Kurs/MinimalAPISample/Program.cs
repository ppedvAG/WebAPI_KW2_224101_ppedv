using FluentValidation;

using Microsoft.EntityFrameworkCore;
using MinimalAPISample.Data;
using MinimalAPISample.Models;
using static Microsoft.AspNetCore.Http.Results;


var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddValidatorsFromAssemblyContaining<Program>();
#region IOC Container -> ServiceCollection wird befüllt

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GeoDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("GeoDbConcectionString"));
    options.UseLazyLoadingProxies(); //Relationen können wir via Lazy Loading erlangen -> anderes Konzpt wäre Eager-Loading
});



var app = builder.Build();
#endregion


#region Konfiguration
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
#endregion

#region Einstieg

#region Einfache Übersicht
app.MapGet("/", () => "This is a GET");
app.MapPost("/", () => "This is a POST");
app.MapPut("/", () => "This is a PUT");
app.MapDelete("/", () => "This is a DELETE");

app.MapMethods("/options-or-head", new[] { "OPTIONS", "HEAD" },
                          () => "This is an options or head request ");

#endregion

#region Lambda Expression
string LocalFunction() => "This is local function";

app.MapGet("/", LocalFunction);
#endregion


#region Instance Methoden verwenden
HelloHandler helloHandler = new HelloHandler();

app.MapGet("/InstanceSample", helloHandler.Hello);
#endregion
#endregion

#region CRUD - Continents

app.MapGet("/GetAllContinents", async (GeoDbContext db) =>
{
    return Results.Ok(await db.Continents.ToListAsync());
});

app.MapPost("/NewContinent", async (Continent continent, GeoDbContext db, HttpContext context) =>
{
    db.Continents.Add(continent);
    await db.SaveChangesAsync();

    return Results.Created<Continent>("/NewContinent", continent);
});

app.MapPut("/UpdateContinent", async (int id, Continent modifiedContinent, GeoDbContext db) =>
{
    if (id != modifiedContinent.Id)
    {
        Results.BadRequest("Was hast du angestellt");
    }

    if (modifiedContinent == null)
        Results.BadRequest("Continent ist null");


    Continent orginalContinent = await db.Continents.FindAsync(id);
    orginalContinent.Name = modifiedContinent.Name;

    await db.SaveChangesAsync();
});

app.MapDelete("/DeleteContinent/{id}", async (int id, GeoDbContext context) =>
{

    Continent continentToDelete = await context.Continents.FindAsync(id);

    if (continentToDelete is not null)
    {
        context.Continents.Remove(continentToDelete);
        await context.SaveChangesAsync();
        return Results.Ok(continentToDelete);
    }

    return Results.NotFound();
});
#endregion



#region Route Constraint

//https://www.learnrazorpages.com/razor-pages/routing
//https://www.learnrazorpages.com/miscellaneous/constraints
//app.MapGet("/todos/{id:int}", (int id) => db.Todos.Find(id));
//app.MapGet("/todos/{text}", (string text) => db.Todos.Where(t => t.Text.Contains(text));
//app.MapGet("/posts/{slug:regex(^[a-z0-9_-]+$)}", (string slug) => $"Post {slug}");
#endregion

app.Run();


class HelloHandler
{
    public string Hello()
    {
        return "Hello Instance method";
    }
}







