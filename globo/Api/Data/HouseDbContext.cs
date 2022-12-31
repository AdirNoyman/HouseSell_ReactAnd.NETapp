using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class HouseDbContext : DbContext
    {

        public HouseDbContext(DbContextOptions<HouseDbContext> o) : base(o) { }

        public DbSet<HouseEntity> Houses => Set<HouseEntity>();

        public DbSet<BidEntity> Bids => Set<BidEntity>();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            options.UseSqlite($"Data Source={Path.Join(path, "houses2.db")}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData.Seed(modelBuilder);
        }


    }
}