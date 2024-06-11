using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Context
{


  


    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Address> addresses { get; set; }
    //    public DbSet<AppUser> users { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Color> colors{ get; set; }
        public DbSet<Size> sizes{ get; set; }
        public DbSet<Images> images{ get; set; }
        public DbSet<Item> items{ get; set; }
        public DbSet<itemReview> itemReviews{ get; set; }
        public DbSet<VendorReviews> vendorReviews{ get; set; }
        public DbSet<itemsInOrdercs> ItemsInOrdercs{ get; set; }





        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        /*  protected override void OnModelCreating(ModelBuilder builder)
          {

                          builder.Entity<AppUser>()
                          .HasOne(u => u.address)
                          .WithOne(a => a.User)
                          .HasForeignKey<Address>(a => a.UserID)
                          .OnDelete(DeleteBehavior.Cascade);
                          base.OnModelCreating(builder);

                      }
              */

    }
}
