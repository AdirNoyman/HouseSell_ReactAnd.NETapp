using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{

    public interface IHouseRepository
    {

        Task<List<HouseDto>> GetAll();
    }


    public class HouseRepository : IHouseRepository
    {
        private readonly HouseDbContext context;
        public HouseRepository(HouseDbContext context)
        {

            this.context = context;

        }

        public async Task<List<HouseDto>> GetAll()
        {

            return await context.Houses.Select(house => new HouseDto(house.Id, house.Address, house.Country, house.Price)).ToListAsync();

        }


    }

}