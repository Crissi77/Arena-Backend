using AutoMapper;
using SportsArena.Enums.Enum;
using SportsArena.Models.DbModels;
using SportsArena.Models.DTOModels;

namespace SportsArenaWeb.AutoMapper
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<SportsDto,UserDetails>();
            CreateMap<SportsDto, UserAddress>();
            CreateMap<UserDetails,SportsDto>();
            
            CreateMap<UserAddress,SportsDto>();
            CreateMap<productDto, Product>()
               .ForMember(dest => dest.ProductId, opt => opt.Ignore());
               

            CreateMap<Product, productDto>();

    //        CreateMap<Product, productDto>()
    //.ForMember(dest => dest.Category, opt => opt.MapFrom(src => Enum.Parse<Category>(src.Category)));


        }
    }
}
