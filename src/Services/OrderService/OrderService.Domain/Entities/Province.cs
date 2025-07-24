namespace OrderService.Domain.Entities;

public class Province : BaseEntity
{
    public string Name { get; set; }
    public Guid CountryId { get; set; }
    public Country Country { get; set; }
}