using AutoMapper;
using AutoMapper.Configuration.Conventions;

namespace ContactBook.Application.Services.Mapper;

/// <summary>
/// Mapper service
/// </summary>
public class MapperService
{
    private readonly IMapper _mapper;

    public MapperService(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Map a source object to a destination object
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return _mapper.Map<TDestination>(source);
    }
}