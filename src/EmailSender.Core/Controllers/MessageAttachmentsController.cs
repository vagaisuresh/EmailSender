using AutoMapper;
using EmailSender.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageAttachmentsController : ControllerBase
{
    private readonly IMessageAttachmentService _service;
    private readonly IMapper _mapper;
    private readonly ILoggerService _logger;
    
    public MessageAttachmentsController(IMessageAttachmentService service, IMapper mapper, ILoggerService logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }
}