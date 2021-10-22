namespace _3K1SPZ_CP.DAL;

public class OrderServices
{
    private OrderRepository orderRepository { get; set; }
    public OrderServices()
    {
        orderRepository = new OrderRepository(Helper.CnnVal());
    }
    public void PlaceOrder(int userId, int productId) => orderRepository.CreateOrder(userId, productId);
    public OrderDTO GetOrder(int id) => orderRepository.Get(id);
    public List<OrderDTO> GetOrderHistory(int userId) => orderRepository.GetOrderHistory(userId);
    public bool UserOrderCheck(int userId, int orderId)
    {
        OrderDTO order;
        if ((order = orderRepository.Get(orderId)) != null)
            if (order.UserId == userId)
                return true;
        return false;
    }
}