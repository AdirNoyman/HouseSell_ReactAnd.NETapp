using Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<HouseDbContext>(o => o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddScoped<IHouseRepository, HouseRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(p => p.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials());

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/houses", (IHouseRepository repo) => repo.GetAllHouses())
// Let Swagger know what this API can return
.Produces<HouseDto[]>(StatusCodes.Status200OK);

app.MapGet("/houses/{houseId:int}", async (int houseId, IHouseRepository repo) =>
{
    var house = await repo.GetOneHouse(houseId);
    if (house == null)
    {
        return Results.Problem($"House with Id {houseId}, was not found 😩.", statusCode: 404);
    }

    return Results.Ok(house);

    // Let Swagger know what this API can return
}).ProducesProblem(404).Produces<HouseDetailDto>(StatusCodes.Status200OK);

app.Run();


