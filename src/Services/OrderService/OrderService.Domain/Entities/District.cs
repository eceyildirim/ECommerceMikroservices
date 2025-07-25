namespace OrderService.Domain.Entities;

public class District : BaseEntity
{
    public string Name { get; set; }
    public Guid ProvinceId { get; set; }
    public Province Province { get; set; }
    public ICollection<Neighborhood> Neighborhoods { get; set; } = new List<Neighborhood>();
}