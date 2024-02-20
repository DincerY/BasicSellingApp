namespace OrderApi.Repository.Entities;

public class Product
{
    public int Id { get; set; }
    public double Price { get; set; }
    public string Name { get; set; }
    public string Desciption { get; set; }
    public string Brand { get; set; }
    public string Category { get; set; }
    public ICollection<OrderProduct> Orders { get; set; }

}