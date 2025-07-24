namespace OrderService.Domain.Entities;

public class Address : BaseEntity
{
    public Guid CustomerId { get; set; }
    public Guid CountryId { get; set; }
    public Guid ProvinceId { get; set; }
    public Guid DistrictId { get; set; }
    public Guid NeighborhoodId { get; set; }
    public string  AddressLine { get; set; }
    public string  AddressLine2 { get; set; }
    public string PostalCode { get; set; }
    public Country Country { get; set; }
    public Province Province { get; set; }
    public District District { get; set; }
    public Neighborhood Neighborhood { get; set; }
}