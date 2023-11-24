namespace hohland_cblp.ShopBackend.Domain.Entities;

public record Product()
{
    public long Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public string Currency { get; init; }
    public ProductType ProductType { get; init; }
    public DateTime CreationDate { get; init; }
}