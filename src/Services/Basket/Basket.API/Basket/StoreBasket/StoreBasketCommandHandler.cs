using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto) 
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.Cart;

            await DeductDiscount(cart, cancellationToken);

            var basket = await repository.StoreBasket(cart, cancellationToken);

            return new StoreBasketResult(basket.UserName);
        }

        private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellation)
        {
            foreach (var item in cart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest
                {
                    ProductName = item.ProductName
                }, cancellationToken: cancellation);

                item.Price -= coupon.Amount;
            }
        }
    }
}
