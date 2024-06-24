using AutoMapper;
using AutoMapper.Configuration.Conventions;

namespace ContactBook.Application.Services.Mapper;

public class MapperService
{
    private readonly IMapper _mapper;

    public MapperService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return _mapper.Map<TDestination>(source);
    }
}