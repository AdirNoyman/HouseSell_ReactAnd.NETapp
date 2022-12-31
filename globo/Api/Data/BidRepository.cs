using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public interface IBidRepository
    {
        Task<List<BidDto>> GetHouseBids(int houseId);
        Task<BidDto> AddBidToHouse(BidDto bid);
    }
    public class BidRepository : IBidRepository
    {

        private readonly HouseDbContext context;
        public BidRepository(HouseDbContext context)
        {

            this.context = context;

        }

        public async Task<List<BidDto>> GetHouseBids(int houseId)
        {
            return await context.Bids.Where(b => b.HouseId == houseId).Select(b => new BidDto(b.Id, b.HouseId, b.Bidder, b.Amount)).ToListAsync();
        }

        public async Task<BidDto> AddBidToHouse(BidDto dto)
        {
            var entity = new BidEntity();
            entity.HouseId = dto.HouseId;
            entity.Bidder = dto.Bidder;
            entity.Amount = dto.Amount;
            context.Bids.Add(entity);
            await context.SaveChangesAsync();
            return new BidDto(entity.Id, entity.HouseId, entity.Bidder, entity.Amount);

        }

    }
}