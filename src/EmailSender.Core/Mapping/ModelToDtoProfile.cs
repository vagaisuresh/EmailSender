using AutoMapper;
using EmailSender.Core.Application.DTOs;
using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Mapping;

public class ModelToDtoProfile : Profile
{
    public ModelToDtoProfile()
    {
        CreateMap<EmailAccount, EmailAccountDto>();

        CreateMap<ContactGroupMaster, ContactGroupDto>();
        CreateMap<ContactMaster, ContactMasterDto>();

        CreateMap<Message, MessageDto>();
        CreateMap<MessageAttachment, MessageAttachmentDto>();
        CreateMap<MessageRecipient, MessageRecipientDto>();
    }
}