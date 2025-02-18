

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler
        (IApplicationDbContext dbContext)
        : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);
            var order = await dbContext.Orders
                .FindAsync([orderId], cancellationToken: cancellationToken);

            if (order == null)
            {
                throw new OrderNotFoundException(command.Order.Id);
            }

            UpdateOrderWithValues(order, command.Order);

            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync();

            return new UpdateOrderResult(true);
        }

        private void UpdateOrderWithValues(Order order, OrderDto orderDto)
        {
            var updatedShippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.City, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode, orderDto.ShippingAddress.Country);
            var updatedBillingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.City, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode, orderDto.BillingAddress.Country);
            var updatedPayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

            order.Update(
                orderName: OrderName.Of(orderDto.OrderName),
                shippingAddress: updatedShippingAddress,
                billingAddress: updatedBillingAddress,
                payment: updatedPayment,
                orderStatus: orderDto.Status
                );
        }
    }
}
