using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace WebProject_ECommerce.Models
{
    public class ProjectContext : DbContext
    {

        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }



        public DbSet<Admin> tblAdmin { get; set; }
        public DbSet<Product> tblProducts { get; set; }
        public DbSet<Category> tblCategory { get; set; }
        public DbSet<User> tblUser { get; set; }


        public DbSet<Order> tblOrder { get; set; }
        public DbSet<CartItem> tblCartItem { get; set; }






        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Admin>().HasData(
                new Admin { Email = "sumansen@gmail.com", Password = "Suman@123" }
                );


            modelBuilder.Entity<Category>().HasData(
                new Category { CatId = 1, CatName = "Electronics" },
                new Category { CatId = 2, CatName = "Clothing" }
                );




        }
    }
}
