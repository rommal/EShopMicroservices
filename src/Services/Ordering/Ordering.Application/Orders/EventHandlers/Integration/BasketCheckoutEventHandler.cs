using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger) : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

            await CreateOrder(context.Message);
        }

        private async Task CreateOrder(BasketCheckoutEvent message)
        {
            var command = message.ToCreateOrderCommand();

            await sender.Send(command);
        }
    }
}
