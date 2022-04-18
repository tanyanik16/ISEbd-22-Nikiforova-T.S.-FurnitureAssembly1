using FurnitureAssemblyDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;


namespace FurnitureAssemblyDatabaseImplement
{
    public class FurnitureAssemblyDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=FurnitureAssemblyDatabase;Trusted_Connection=True");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Component> Components { set; get; }
        public virtual DbSet<Furniture> Furnitures{ set; get; }
        public virtual DbSet<FurnitureComponent> FurnitureComponents { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
    }

}
