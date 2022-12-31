using System.ComponentModel.DataAnnotations;

// Record is like a struct or a POJO (POCO)
public record BidDto(int Id, int HouseId, [property: Required] string Bidder, int Amount);