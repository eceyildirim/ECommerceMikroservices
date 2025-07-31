using AutoMapper;
using NotificationService.Domain.Entities;
using NotificationService.Common.Models;

namespace NotificationService.Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<NotificationLogDto, NotificationLog>();
    }
}