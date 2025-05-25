using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
        }
        public virtual DbSet<Coupon> Coupons { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /// Hena h3ml seed data 3shan lma a3ml migration y3mly seed data 
            /// owe kman h3ml auto migrate  awl ma l application yshtghl

            modelBuilder.Entity<Coupon>().HasData(

               new Coupon { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 },
                    new Coupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 100 }
            );
        }



    }
}
