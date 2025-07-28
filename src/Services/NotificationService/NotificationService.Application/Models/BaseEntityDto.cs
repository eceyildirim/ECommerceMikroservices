namespace NotificationService.Application.Models;

public class BaseEntityDto
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
}