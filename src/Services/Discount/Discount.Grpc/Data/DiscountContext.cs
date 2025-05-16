using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Discount.Grpc.Data
{
    public class DiscountContext  : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
        }
        public virtual DbSet<Coupon> Coupons { get; set; } = default!;
       


    }
}
