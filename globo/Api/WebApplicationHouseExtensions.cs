using Api.Data;
using Microsoft.AspNetCore.Mvc;
using MiniValidation;

public static class WebApplicationHouseExtensions
{
    public static void MapHouseEndPoints(this WebApplication app)
    {

        // GET ALL  houses
        app.MapGet("/houses", (IHouseRepository repo) => repo.GetAllHouses())
        // Let Swagger know what this API endpoint will return
        .Produces<HouseDto[]>(StatusCodes.Status200OK);

        // GET ONE house
        app.MapGet("/houses/{houseId:int}", async (int houseId, IHouseRepository repo) =>
        {
            var house = await repo.GetOneHouse(houseId);
            if (house == null)
            {
                return Results.Problem($"House with Id {houseId}, was not found 😩.", statusCode: 404);
            }

            return Results.Ok(house);

            // Let Swagger know what this API endpoint will return
        }).ProducesProblem(404).Produces<HouseDetailDto>(StatusCodes.Status200OK);

        // CREATE a new house
        // [FromBody] - tells .net to look for HouseDetailDto in the request body
        app.MapPost("/houses", async ([FromBody] HouseDetailDto dto, IHouseRepository repo) =>
        {

            // Validate user's input
            // errors is a dictionary of key and value of string array. Key is the name of the input field and the value is the validation errors
            if (!MiniValidator.TryValidate(dto, out var errors))
            {
                return Results.ValidationProblem(errors);
            }
            var newHouse = await repo.AddNewHouse(dto);
            return Results.Created($"/houses/{newHouse.Id}", newHouse);
        }
        // Let Swagger know what this API endpoint will return
        ).Produces<HouseDetailDto>(StatusCodes.Status201Created)
        .ProducesValidationProblem();

        // UPDATE a house
        // [FromBody] - tells .net to look for HouseDetailDto in the request body
        app.MapPut("/houses", async ([FromBody] HouseDetailDto dto, IHouseRepository repo) =>
        {
            // Validate user's input
            // errors is a dictionary of key and value of string array. Key is the name of the input field and the value is the validation errors
            if (!MiniValidator.TryValidate(dto, out var errors))
            {
                return Results.ValidationProblem(errors);
            }

            if (await repo.GetOneHouse(dto.Id) == null)
            {
                return Results.Problem($"Update failed. House with {dto.Id} was not found 😩.", statusCode: 404);
            }

            var updatedHouse = await repo.UpdateHouse(dto);
            return Results.Ok(updatedHouse);

        }
        // Let Swagger know what this API endpoint will return
        ).ProducesProblem(404).Produces<HouseDetailDto>(StatusCodes.Status200OK)
        .ProducesValidationProblem();

        // DELETE a house
        // [FromBody] - tells .net to look for HouseDetailDto in the request body
        app.MapDelete("/houses/{houseId:int}", async (int houseId, IHouseRepository repo) =>
        {

            if (await repo.GetOneHouse(houseId) == null)
            {
                return Results.Problem($"Delete failed. House with {houseId} was not found 😩.", statusCode: 404);
            }

            await repo.DeleteHouse(houseId);
            return Results.Ok($"House with id {houseId} was deleted 😁🤟");

        }
        // Let Swagger know what this API endpoint will return
        ).ProducesProblem(404).Produces(StatusCodes.Status200OK);


    }
}