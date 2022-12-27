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
        Task<HouseDetailDto> AddNewHouse(HouseDetailDto dto);
        Task<HouseDetailDto> UpdateHouse(HouseDetailDto dto);
        Task DeleteHouse(int id);


    }


    public class HouseRepository : IHouseRepository
    {
        private readonly HouseDbContext context;
        public HouseRepository(HouseDbContext context)
        {

            this.context = context;

        }

        // Transform house dto data to db house entity function
        private static void DtoToEntity(HouseDetailDto dto, HouseEntity e)
        {

            e.Address = dto.Address;
            e.Country = dto.Country;
            e.Description = dto.Description;
            e.Price = dto.Price;
            e.Photo = dto.Photo;
        }

        // Converting hose entity to dto
        private static HouseDetailDto EntityToDetailDto(HouseEntity e)
        {
            // Converting the house entity to house dto
            return new HouseDetailDto(e.Id, e.Address, e.Country, e.Description, e.Price, e.Photo);
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
            return EntityToDetailDto(e);

        }

        // Add new house
        public async Task<HouseDetailDto> AddNewHouse(HouseDetailDto dto)
        {

            var entity = new HouseEntity();
            DtoToEntity(dto, entity);
            context.Houses.Add(entity);
            // Save to the DB
            await context.SaveChangesAsync();
            return EntityToDetailDto(entity);
        }

        // Update a house
        public async Task<HouseDetailDto> UpdateHouse(HouseDetailDto dto)
        {
            var entity = await context.Houses.FindAsync(dto.Id);
            if (entity == null)
            {
                throw new ArgumentException($"Error updating house {dto.Id} 😩");

            }

            DtoToEntity(dto, entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return EntityToDetailDto(entity);

        }

        // Delete a house
        public async Task DeleteHouse(int id)
        {
            var entity = await context.Houses.FindAsync(id);
            if (entity == null)
            {
                throw new ArgumentException($"Error deleting the house {id} 😩");

            }

            context.Houses.Remove(entity);
            await context.SaveChangesAsync();

        }

    }

}