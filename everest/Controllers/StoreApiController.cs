using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using everest.DTOs;
using everest.Entities;
using everest.Interfaces;
using everest.Services;
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

            if (store.StorePhotoId != 0)
            {
                var storePhotoStore = await  _uof.StoreRepository.GetStorePhotoAsync(store);

                var publicId = storePhotoStore.PublicId;

                var deletionResult = await _phtotoServices.RemovePhoto(publicId);

                if (deletionResult.Error != null) return BadRequest(deletionResult.Error.Message);

                var uploadResult = await _phtotoServices.AddStorePhotoAsync(file);

                if (uploadResult.Error != null) return BadRequest(uploadResult.Error.Message);




                storePhotoStore.Url = uploadResult.Url.AbsoluteUri;
                storePhotoStore.PublicId = uploadResult.PublicId;

                if (!await _uof.Complete()) return BadRequest("Field to update company photo");

                return NoContent();

            }

            var result = await _phtotoServices.AddStorePhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var storePhoto = new StorePhoto
            {
                Url = result.Url.AbsoluteUri,
                PublicId = result.PublicId,
                StoreId = store.Id,
                Store = store
            };

            await _uof.StoreRepository.AddStorePhotoAsync(store, storePhoto);
            

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



        [HttpPost("add-product")]
        public async Task<ActionResult<ProductDto>> AppProduct([FromBody]ProductDto productDto)
        {
            var user = await _userManager.GetUserAsync(User);
            var store = await _uof.StoreRepository.GetStoreAsync(user);

            productDto.PublicId = GeneratorId.CreateRandomId(6);

            var product = _mapper.Map<Product>(productDto);


            while (await _uof.StoreRepository.GetProductByPublicIdAsync(product.PublicId) != null)
            {
                product.PublicId = GeneratorId.CreateRandomId(6);
            }

            store.Products.Add(product);


            if (await _uof.Complete())
            {
                productDto.PublicId = product.PublicId;
                return productDto;
            } 

            return BadRequest("Field to product");
        }


        [HttpPost("add-product-photos")]
        public async Task<IActionResult> AddProductPhotos([FromForm]IFormFile[] files,string productId)
        {
            var product = await _uof.StoreRepository.GetProductByPublicIdAsync(productId);
            var mainFile = files.FirstOrDefault(f => f.FileName == "file-image1");

            if (mainFile == null) return BadRequest("You have to add the main photo");


            //upload main photo
            var mainUpload = await _phtotoServices.AddProductPhotoAsync(mainFile);

            if (mainUpload.Error != null) return BadRequest(mainUpload.Error.Message);

            var productMainPhoto = new ProductPhoto
            {
                Url = mainUpload.Url.AbsoluteUri,
                IsMain = true,
                PublicId = mainUpload.PublicId
            };

            product.Photos.Add(productMainPhoto);

            if (!await _uof.Complete()) return BadRequest("Filed to upload main photo");

            //Add another product photos
            foreach(var file in files)
            {
                if (file == mainFile) continue;

                var uploadResult = await _phtotoServices.AddProductPhotoAsync(file);

                if (uploadResult.Error != null) return BadRequest(uploadResult.Error.Message);

                var productPhoto = new ProductPhoto
                {
                    Url = uploadResult.Url.AbsoluteUri,
                    IsMain = false,
                    PublicId = uploadResult.PublicId
                };

                product.Photos.Add(productPhoto);

                if (!await _uof.Complete()) BadRequest("Filed to upload image: " + file.FileName);
            }
            

            return NoContent();
        }

    }
}
