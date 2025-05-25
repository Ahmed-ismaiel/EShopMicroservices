using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService
        /// Hstkhdm l database 3shan atb2 3leha l discount service mn l proto file 
        (DiscountContext dbContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            // HEna bgeb l data mn l database 3n tareq l product name

            var coupon = await dbContext
                .Coupons
                .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon == null)
                coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
            logger.LogInformation($"Discount is retured for the product {coupon.ProductName} with discount {coupon.Amount}");

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;


        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {

            // lw request gy fady hrmy exception gher kda hdefo 3ndy fl database
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found"));
            }

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation($"Discount is created for the product {coupon.ProductName} with discount {coupon.Amount}");
            return coupon.Adapt<CouponModel>();



        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found"));
            }

            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation($"Discount is Updated for the product {coupon.ProductName} with discount {coupon.Amount}");
            return coupon.Adapt<CouponModel>();
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext
             .Coupons
             .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon is null)

            {
                throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found"));

            }
            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation($"Discount is deleted for the product {coupon.ProductName} with discount {coupon.Amount}");

            return new DeleteDiscountResponse
            {
                Success = true
            };

        }


    }
}
