using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{

    public interface IHouseRepository
    {

        Task<List<HouseDto>> GetAllHouses();
        Task<HouseDetailDto?> GetOneHouse(int id);
    }


    public class HouseRepository : IHouseRepository
    {
        private readonly HouseDbContext context;
        public HouseRepository(HouseDbContext context)
        {

            this.context = context;

        }

        public async Task<List<HouseDto>> GetAllHouses()
        {

            return await context.Houses.Select(house => new HouseDto(house.Id, house.Address, house.Country, house.Price)).ToListAsync();

        }

        public async Task<HouseDetailDto?> GetOneHouse(int id)
        {

            var e = await context.Houses.SingleOrDefaultAsync(house => house.Id == id);

            if (e == null) return null;

            // Converting the house entity to house dto
            return new HouseDetailDto(e.Id, e.Address, e.Country, e.Description, e.Price, e.Photo);

        }



    }

}