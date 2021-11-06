using AutoMapper;
using everest.Areas.Identity.Pages.Account.MyProfile.StoreManager;
using everest.DTOs;
using everest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static everest.Areas.Identity.Pages.Account.MyProfile.StoreManager.StoreInfoModel;

namespace everest.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Store, InputModel>()
                .ForMember(des => des.StorePhotoUrl, opt => opt.MapFrom(src => src.CompanyPhoto.Url));
            CreateMap<IEnumerable<Classification>, IEnumerable<ClassificationDto>>();
            CreateMap<ClassificationDto, Classification>();
            CreateMap<ClientDto, Store>();
                
        }
    }
}
