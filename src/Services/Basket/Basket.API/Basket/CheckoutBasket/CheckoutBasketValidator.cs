namespace Basket.API.Basket.CheckoutBasket
{
    public class CheckoutBasketValidator: AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketValidator() 
        {
            RuleFor(x => x.BasketCheckout).NotNull().WithMessage("BasketCheckout can't be null.");
            RuleFor(x => x.BasketCheckout.UserName).NotEmpty().WithMessage("UserName is required.");
        }
    }
}
