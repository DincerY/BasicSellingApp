namespace OrderApi.Repository.Entities;

public class Order
{
    public int Id { get; set; }
    public int BuyerId { get; set; }
    public DateTime OrderDate { get; set; }
    public Buyer Buyer { get; set; }
    public ICollection<OrderProduct> Products { get; set; }
}