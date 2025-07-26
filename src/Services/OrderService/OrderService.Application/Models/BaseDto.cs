namespace OrderService.Application.Models;

public class BaseDto
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
}