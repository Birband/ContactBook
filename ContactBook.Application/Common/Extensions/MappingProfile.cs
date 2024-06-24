using AutoMapper;
using ContactBook.Application.DTOs;
using ContactBook.Domain.Entities;

namespace ContactBook.Application.Common.Extensions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Contact mapping
        CreateMap<Contact, ContactDto>().ReverseMap();

        CreateMap<Contact, ContactDto>().ReverseMap();

        // Category mapping
        CreateMap<Category, CategoryDto>().ReverseMap();

        CreateMap<CategoryDto, Category>().ReverseMap();

        // SubCategory mapping
        CreateMap<Subcategory, SubcategoryDto>().ReverseMap();

        CreateMap<SubcategoryDto, Subcategory>().ReverseMap();
    }
}
