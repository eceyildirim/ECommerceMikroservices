namespace OrderService.Domain.Entities;

public class Neighborhood : BaseEntity
{
    public string Name { get; set; }
    public Guid DistrictId { get; set; }
    public District District { get; set; }
}