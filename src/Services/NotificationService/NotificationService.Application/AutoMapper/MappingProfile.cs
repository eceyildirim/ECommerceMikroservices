using AutoMapper;
using NotificationService.Domain.Entities;
using NotificationService.Application.Models;

namespace NotificationService.Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<NotificationLogDto, NotificationLog>();
    }
}