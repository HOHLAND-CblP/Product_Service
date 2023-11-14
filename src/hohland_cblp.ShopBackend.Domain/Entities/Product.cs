namespace hohland_cblp.ShopBackend.Domain.Entities;

public record Product()
{
    
    public long Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public float Weight { get; set; }
    public ProductType ProductType { get; set; }
    public DateTime CreationDate { get; set; }
    public long WarehouseId { get; set; }
}