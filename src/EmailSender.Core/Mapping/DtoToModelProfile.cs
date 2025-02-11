using AutoMapper;
using EmailSender.Core.Application.DTOs;
using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Mapping;

public class DtoToModelProfile : Profile
{
    public DtoToModelProfile()
    {
        CreateMap<EmailAccountSaveDto, EmailAccount>();
    }
}