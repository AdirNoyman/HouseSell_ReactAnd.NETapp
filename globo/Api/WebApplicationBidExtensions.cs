using Api.Data;
using Microsoft.AspNetCore.Mvc;
using MiniValidation;

public static class WebApplicationBidExtensions
{
    public static void MapBidEndPoints(this WebApplication app)
    {

        app.MapGet("/houses/{houseId:int}/bids", async (int houseId, IHouseRepository houseRepo, IBidRepository bidRepo) =>
{
    if (await houseRepo.GetOneHouse(houseId) == null)
    {
        return Results.Problem($"House with ID {houseId}, was not found 😩", statusCode: 404);
    }

    var bids = await bidRepo.GetHouseBids(houseId);
    return Results.Ok(bids);
}
).ProducesProblem(404).Produces(StatusCodes.Status200OK);

        app.MapPost("/houses/{houseId:int}/bids", async (int houseId, [FromBody] BidDto dto, IBidRepository bidRepo) =>
        {
            if (dto.HouseId != houseId)
            {
                return Results.Problem("No Match between house Id in the url to the house Id sent in request body", statusCode: StatusCodes.Status400BadRequest);
            }

            if (!MiniValidator.TryValidate(dto, out var errors))
            {
                return Results.ValidationProblem(errors);
            }

            var newBid = await bidRepo.AddBidToHouse(dto);
            // Return a link to the new bid
            return Results.Created($"/houses/{newBid.HouseId}/bids", newBid);

        }
        ).ProducesValidationProblem().ProducesProblem(400).Produces<BidDto>(StatusCodes.Status201Created);

    }


}