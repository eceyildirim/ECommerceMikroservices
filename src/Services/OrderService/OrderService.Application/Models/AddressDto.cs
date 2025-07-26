namespace OrderService.Application.Models;

public class AddressDto : BaseDto
{
    public string AddressLine { get; set; }
    public string AddressLine2 { get; set; }
    public string PostalCode { get; set; }
    public CountryDto Country { get; set; }
    public ProvinceDto Province { get; set; }
    public DistrictDto District { get; set; }
    public NeighborhoodDto Neighborhood { get; set; }
    public CustomerDto Customer { get; set;}
}