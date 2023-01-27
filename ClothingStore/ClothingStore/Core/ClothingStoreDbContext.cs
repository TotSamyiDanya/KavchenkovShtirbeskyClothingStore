using ClothingStore.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace ClothingStore.Core
{
    public class ClothingStoreDbContext : DbContext
    {
        public DbSet<Cloth> Clothes { get; set; }
        public DbSet<ClothQuantity> ClothQuantities { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<OrderCloth> OrderClothes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public ClothingStoreDbContext()
        {
            Database.EnsureCreated();
            //Database.EnsureDeleted();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cloth>().HasKey(c => c.ClothId);
            modelBuilder.Entity<ClothQuantity>().HasKey(cq => cq.ClothQuantityId);
            modelBuilder.Entity<Store>().HasKey(s => s.StoreId);
            modelBuilder.Entity<OrderCloth>().HasKey(co => co.OrderClothId);
            modelBuilder.Entity<Order>().HasKey(o => o.OrderID);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CreateConnectionString()).UseLazyLoadingProxies();
        }
        private string CreateConnectionString()
        {
            string sql = File.ReadAllText("sqlStringsConnection.json");
            JObject jobj = JObject.Parse(sql);
            return jobj["0"]!.Value<string>()!;
        }
    }
}
