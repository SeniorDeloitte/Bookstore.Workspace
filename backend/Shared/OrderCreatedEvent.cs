namespace Shared
{
    // Define los datos que se enviarán a la cola de RabbitMQ cuando se cree una orden
    public record OrderCreatedEvent
    {
        public int OrderId { get; init; }
        public required string CustomerEmail { get; init; }
        public List<OrderItemEventDto> Items { get; init; } = new();
    }

    public record OrderItemEventDto
    {
        public int BookId { get; init; }
        public int Quantity { get; init; }
    }
}
