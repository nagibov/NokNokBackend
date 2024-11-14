using AutoMapper;
using Catalog.DataLayer.MongoDB.Entities;
using Catalog.Domain.Dtos;

namespace Catalog.DataLayer.MongoDB.Profiles;

public class DtoToEntityMapper : Profile
{
    public DtoToEntityMapper()
    {
        CreateMap<ProductItemDto, ProductItem>().ReverseMap();
        CreateMap<ImageDto, Image>().ReverseMap();
        CreateMap<ProductCustomizationItemDto, ProductCustomizationItem>().ReverseMap();
        CreateMap<ProductVariationItemDto, ProductVariationItem>().ReverseMap();
        CreateMap<ProductCharacteristicsDto, ProductCharacteristics>().ReverseMap();
        CreateMap<UiCustomizationDto, UiCustomization>().ReverseMap();
    }
}