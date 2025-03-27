namespace Basket.API.Basket.CheckoutBasket
{
    public class CheckoutBasketValidator: AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketValidator() 
        {
            RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckout can't be null.");
            RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required.");
        }
    }
}
