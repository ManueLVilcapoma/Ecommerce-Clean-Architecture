using Ecommerce.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence;

public class EcommerceDbContext:IdentityDbContext<Usuario>{


    public EcommerceDbContext(DbContextOptions<EcommerceDbContext>options):base(options){
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        base.OnModelCreating(modelBuilder);
        //Relacion de uno a muchos de categoria con productos
        modelBuilder.Entity<Category>().HasMany(p=>p.Products)
        .WithOne(r => r.Category)
        .HasForeignKey(r => r.CategoryId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Restrict);
        //
        //Cuando trabajes con mysql es muy necesario
        modelBuilder.Entity<Usuario>().Property(e => e.Id).HasMaxLength(36);
        modelBuilder.Entity<Usuario>().Property(e => e.NormalizedUserName).HasMaxLength(90);
        //
        modelBuilder.Entity<IdentityRole>().Property(e => e.Id).HasMaxLength(36);
        modelBuilder.Entity<IdentityRole>().Property(e => e.NormalizedName).HasMaxLength(90);
    }

    public DbSet<Product>? Products{ get; set; }
    public DbSet<Category>? Categories{ get; set; }
    public DbSet<Image>? Images{get;set;}
    public DbSet<Address>? Addresses{ get; set; }
    public DbSet<Order>? Orders{ get; set; }
    public DbSet<OrderItem>? OrderItems{get; set; }
    public DbSet<Review>? Reviews{ get; set; }
    public DbSet<ShoppingCart>? ShoppingCarts{get; set; }
    public DbSet<ShoppingCartItem>? ShoppingCartItems{get; set; }
    public DbSet<Country>? Countries{ get; set; }
    public DbSet<OrderAddress>? OrderAddresses{ get; set; }

}