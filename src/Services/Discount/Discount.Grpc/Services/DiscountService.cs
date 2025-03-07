using Discount.Grpc.Data;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountContext> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            Coupon? coupon = await GetDiscount(request.ProductName);

            return coupon.Adapt<CouponModel>();
        }        

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

            dbContext.Coupons.Add(coupon);

            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is created for Product: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is updated for Product: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = GetDiscount(request.ProductName);

            if (coupon.Id == 0)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));

            dbContext.Coupons.Remove(coupon.Result);
            await dbContext.SaveChangesAsync();

            return new DeleteDiscountResponse
            {
                Success = true
            };
        }

        private async Task<Coupon> GetDiscount(string productName)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == productName);

            if (coupon is null)
                coupon = new Coupon { ProductName = "No Discount", Description = "No Discount" };

            logger.LogInformation("Discount is retrieved for Product: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

            return coupon;
        }
    }
}
