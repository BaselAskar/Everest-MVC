using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using everest.DTOs;
using everest.Entities;
using everest.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace everest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreApiController : ControllerBase
    {
        private readonly IPhotoServices _phtotoServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public StoreApiController(IPhotoServices photoServices,UserManager<AppUser> userManager,IUnitOfWork uof,IMapper mapper)
        {
            _phtotoServices = photoServices;
            _userManager = userManager;
            _uof = uof;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhoto(IFormFile file)
        {
            var user = await _userManager.GetUserAsync(User);
            var store = await _uof.StoreRepository.GetStoreAsync(user);


            if (file == null) return NoContent();

            if (store.CompanyPhotoId != 0)
            {
                var companyPhotoStore = await _uof.StoreRepository.GetCompanyPhotoAsync(user);

                var publicId = companyPhotoStore.PublicId;

                var deletionResult = await _phtotoServices.RemovePhoto(publicId);

                if (deletionResult.Error != null) return BadRequest(deletionResult.Error.Message);

                var uploadResult = await _phtotoServices.AddCompanyPhotoAsync(file);

                if (uploadResult.Error != null) return BadRequest(uploadResult.Error.Message);




                companyPhotoStore.Url = uploadResult.Url.AbsoluteUri;
                companyPhotoStore.PublicId = uploadResult.PublicId;

                if (!await _uof.Complete()) return BadRequest("Field to update company photo");

                return NoContent();

            }

            var result = await _phtotoServices.AddCompanyPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var companyPhoto = new CompanyPhoto
            {
                Url = result.Url.AbsoluteUri,
                PublicId = result.PublicId,
                StoreId = user.StoreId,
                Store = user.Store
            };

            await _uof.StoreRepository.AddCompanyPhotoAsync(user, companyPhoto);

            if (!await _uof.Complete()) return BadRequest("There is a wrong during uploading photo");

            return NoContent();
        }



        [HttpPut]
        public async Task<IActionResult> UpdateInfo([FromBody] ClientDto clientDto)
        {
            var user = await _userManager.GetUserAsync(User);

            var store = await _uof.StoreRepository.GetStoreAsync(user);

            _mapper.Map(clientDto, store);

            _uof.StoreRepository.UpdateStore(store);

            if (!await _uof.Complete()) return BadRequest("There is a wrong during updating store information");

            return NoContent();
        }

    }
}
