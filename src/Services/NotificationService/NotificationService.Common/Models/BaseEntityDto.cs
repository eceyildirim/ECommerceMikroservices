namespace NotificationService.Common.Models;

public class BaseEntityDto
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
}