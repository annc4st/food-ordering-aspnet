using cafeMvc.Models;
 
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cafeMvc.Data
{
    public class CafeMvcContext : DbContext
    {
        public CafeMvcContext(DbContextOptions<CafeMvcContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           //dish
            modelBuilder.Entity<DishIngredient>()
            .HasKey(di => new { di.DishId, di.IngredientId});


            modelBuilder.Entity<DishIngredient>()
             .HasOne(di => di.Dish)
            .WithMany(d => d.DishIngredients)
            .HasForeignKey(di => di.DishId);
            
            modelBuilder.Entity<DishIngredient>()
            .HasOne(i => i.Ingredient)
            .WithMany(di => di.DishIngredients)
            .HasForeignKey(i => i.IngredientId);

            modelBuilder.Entity<User>()
            .HasOne(u => u.Customer)
            .WithOne(c => c.User)
            .HasForeignKey<Customer>(c => c.UserId);

             modelBuilder.Entity<User>()
             .HasMany(u => u.Roles)          // A User has many Roles
            .WithMany(r => r.Users);        // A Role has many Users



                modelBuilder.Entity<Dish>().HasData(
                new Dish { Id=1, Name= "Margheritta", 
                Price= 7.50, 
                ImageUrl= "https://cdn.shopify.com/s/files/1/0205/9582/articles/20220211142347-margherita-9920_ba86be55-674e-4f35-8094-2067ab41a671.jpg?crop=center&height=915&v=1644590192&width=1200" }
                );

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id= 1, Name="Tomato Sauce"},
                new Ingredient { Id = 2, Name = "Mozzarella" }
                );
            modelBuilder.Entity<DishIngredient>().HasData(
                new DishIngredient { DishId=1, IngredientId=1},
                new DishIngredient { DishId = 1, IngredientId = 2 }
                );

            base.OnModelCreating(modelBuilder);
    }

        public DbSet<Dish> Dishes { get; set; }  
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}