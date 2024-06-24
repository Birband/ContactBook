using AutoMapper;
using ContactBook.Application.DTOs;
using ContactBook.Domain.Entities;

namespace ContactBook.Application.Common.Extensions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Contact, ContactDto>().ReverseMap();

        CreateMap<Contact, ContactDto>().ReverseMap();
    }
}
