
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckout) : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);

    public class CheckoutBasketHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint)
        : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasket(command.BasketCheckout.UserName, cancellationToken);
            if (basket == null) 
            {
                return new CheckoutBasketResult(false);
            }

            var eventmessage = command.BasketCheckout.Adapt<BasketCheckoutEvent>();
            eventmessage.TotalPrice = basket.TotalPrice;

            await publishEndpoint.Publish(eventmessage, cancellationToken);

            await repository.DeleteBasket(command.BasketCheckout.UserName, cancellationToken);

            return new CheckoutBasketResult(true);
        }
    }
}
