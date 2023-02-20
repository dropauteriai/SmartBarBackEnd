using Microsoft.EntityFrameworkCore;
using Persistence;
using Microsoft.OpenApi.Models;
using Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Tables") ?? "Data Source=Tables.db";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SmartBar API",
        Description = "Easy bar work",
        Version = "v1"
    });
});

// Add services to the container.

//var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins,
//                      policy =>
//                      {
//                          policy.WithOrigins("https://localhost:44418"); // add the allowed origins  
//                      });
//});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSqlite<SmartBarDb>(connectionString);

builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<SmartBarDbContext>(options =>
 //                options.UseSqlite(@"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SmartBarDB;Integrated Security=True;"));

var app = builder.Build();



//app.UseCors(MyAllowSpecificOrigins);
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "smartbar api v1");
});

app.MapGet("/tables", async (SmartBarDb db) => await db.Tables.ToListAsync());

app.MapPost("/table", async (SmartBarDb db, Table table) =>
{
    await db.Tables.AddAsync(table);
    await db.SaveChangesAsync();
    return Results.Created($"/table/{table.Id}", table);
});

app.MapGet("/table/{id}", async (SmartBarDb db, Guid id) => await db.Tables.FindAsync(id));

app.MapPut("/table/{id}", async (SmartBarDb db, Table updatetable, Guid id) =>
{
    var table = await db.Tables.FindAsync(id);
    if (table is null) return Results.NotFound();
    table.Name = updatetable.Name;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/table/{id}", async (SmartBarDb db, Guid id) =>
{
    var table = await db.Tables.FindAsync(id);
    if (table is null)
    {
        return Results.NotFound();
    }
    db.Tables.Remove(table);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
