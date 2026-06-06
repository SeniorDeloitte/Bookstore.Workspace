using MediatR;
using OrderService.Data;

namespace OrderService.Features.Orders.Commands
{
    public record OrderItemInput(int BookId, int Quantity, decimal UnitPrice);

    public record CreateOrderCommand(
        string CustomerEmail, 
        List<OrderItemInput> Items
    ) : IRequest<int>;

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                CustomerEmail = request.CustomerEmail,
                OrderDate = DateTime.UtcNow,
                TotalAmount = 0
            };

            foreach (var item in request.Items)
            {
                var orderItem = new OrderItem
                {
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                };

                order.Items.Add(orderItem);
                
                order.TotalAmount += item.Quantity * item.UnitPrice;
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}
