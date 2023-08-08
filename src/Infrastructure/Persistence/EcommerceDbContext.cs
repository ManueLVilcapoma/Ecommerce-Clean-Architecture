using Ecommerce.Domain;
using Ecommerce.Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence;

public class EcommerceDbContext:IdentityDbContext<Usuario>{


    public EcommerceDbContext(DbContextOptions<EcommerceDbContext>options):base(options){
        
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken=default){
        var userName="system";
        foreach(var entry in ChangeTracker.Entries<BaseDomainModel>()){
            switch(entry.State){

                case EntityState.Added:
                    entry.Entity.CreateDate=DateTime.Now;
                    entry.Entity.CreatedBy=userName;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate=DateTime.Now;
                    entry.Entity.LastModifiedBy=userName;
                    break;
                    
            }
        }  
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        base.OnModelCreating(modelBuilder);
        //Relacion de uno a muchos de categoria con productos
        modelBuilder.Entity<Category>().HasMany(p=>p.Products)
        .WithOne(r => r.Category)
        .HasForeignKey(r => r.CategoryId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Restrict);
        //Relacion de uno a mucho de products con reviews
        modelBuilder.Entity<Product>()
        .HasMany(p=>p.Reviews)
        .WithOne(r => r.Product)
        .HasForeignKey(r => r.ProductId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

        // relacion de uno a muchos con producto e imagenes
        modelBuilder.Entity<Product>()
        .HasMany(p=>p.Images)
        .WithOne(r => r.Product)
        .HasForeignKey(r => r.ProductId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);
        //Relacion de uno a muchos de shopping cart con shopping cart items
        modelBuilder.Entity<ShoppingCart>()
        .HasMany(p=>p.ShoppingCartItems)
        .WithOne(r => r.ShoppingCart)
        .HasForeignKey(r => r.ShoppingCartId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

        //Observacion ahora toca aprender otra forma de usar EF con ordenes
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