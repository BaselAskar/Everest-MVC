using AutoMapper;
using everest.Areas.Identity.Pages.Account.MyProfile.StoreManager;
using everest.DTOs;
using everest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static everest.Areas.Identity.Pages.Account.MyProfile.StoreManager.StoreInfoModel;
using static everest.Areas.Identity.Pages.Account.MyProfile.StoreManager.EditProductModel;
using everest.ModelsView;
using everest.Extensions;

namespace everest.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Store, InputModel>();
            
            CreateMap<ClassificationDto, StoreClassification>();

            CreateMap<ClientDto, Store>();
            
            CreateMap<ProductDto, Product>();
            
            CreateMap<Product, ProductDto>()
                .ForMember(des => des.MainPhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url));

            CreateMap<ProductPhoto, ProductPhotoDto>();

            CreateMap<Product, InputProductModel>()
                .ForMember(des => des.MainPhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(ph => ph.IsMain).Url));

            CreateMap<SlideForm,Slide>();

            CreateMap<AppUser, UserView>()
                .ForMember(des => des.RoleName, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name).ToList()[0]));

            CreateMap<AppUser, UserDetailsView>();

            CreateMap<Store, ClientView>()
                .ForMember(des => des.Classifications, opt => opt.MapFrom(src => src.Classifications.ConvertToString()));
        }
    }
}
