using CoreLayer.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Context
{
    public class RentCarContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=NIRVANA\SQLEXPRESS;Database=RentCar;Trusted_Connection=True;");
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FavBrand> FavBrands { get; set; }
        public DbSet<FavCar> FavCars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<RentalInfo> Rentals { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<UserCampaign> UserCampaigns { get; set; }
    }
}
