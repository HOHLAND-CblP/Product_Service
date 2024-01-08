using AutoMapper;
using hohland_cblp.ShopBackend.Domain.Entities;
using hohland_cblp.ShopBackend.Infrastructure.DbEntities;
using ProductGrpc;

namespace hohland_cblp.ShopBackend.Infrastructure.AutoMapperProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, Product_v1>()
            .ForMember(item => item.Id,
                opt => opt.MapFrom(from => from.Id))
            .ForMember(item => item.Name,
                opt => opt.MapFrom(from => from.Name))
            .ForMember(item => item.Price,
                opt => opt.MapFrom(from => from.Price))
            .ForMember(item => item.Currency,
                opt => opt.MapFrom(from => from.Currency))
            .ForMember(item => item.ProductType,
                opt => opt.MapFrom(from => from.ProductType))
            .ForMember(item => item.CreationDate,
                opt => opt.MapFrom(from => from.CreationDate))
            .ReverseMap();

        CreateMap<Product, ProductGrpc_v1>()
            .ForMember(item => item.Id,
                opt => opt.MapFrom(from => from.Id))
            .ForMember(item => item.Name,
                opt => opt.MapFrom(from =>from.Id))
            .ForMember(item => item.Price,
                opt => opt.MapFrom(from => from.Price))
            .ForMember(item => item.Currency,
                opt => opt.MapFrom(from => from.Currency))
            .ForMember(item => item.ProductType,
                opt => opt.MapFrom(from => from.ProductType))
            .ForMember(item => item.CreationDate,
                opt => opt.MapFrom(from => from.CreationDate))
            .ReverseMap();
    }
}