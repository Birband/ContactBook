using AutoMapper;
using ContactBook.Application.DTOs;
using ContactBook.Domain.Entities;

namespace ContactBook.Application.Common.Extensions;

/// <summary>
/// Mapping profile for AutoMapper
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Contact mapping
        CreateMap<Contact, ContactDto>().ReverseMap();

        CreateMap<Contact, ContactDto>().ReverseMap();

        // CreateContact mapping
        CreateMap<CreateContactDto, Contact>().ReverseMap();

        CreateMap<CreateContactDto, Contact>().ReverseMap();

        // Category mapping
        CreateMap<Category, CategoryDto>().ReverseMap();

        CreateMap<CategoryDto, Category>().ReverseMap();

        // SubCategory mapping
        CreateMap<Subcategory, SubcategoryDto>().ReverseMap();

        CreateMap<SubcategoryDto, Subcategory>().ReverseMap();
    }
}
