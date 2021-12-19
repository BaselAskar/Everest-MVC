using System;
using System.IO;
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
using System.Text.Json;

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
        public async Task<IActionResult> AppProduct([FromBody]ProductDto productDto)
        {
            var user = await _userManager.GetUserAsync(User);
            var store = await _uof.StoreRepository.GetStoreAsync(user);

            productDto.Id = GeneratorId.CreateRandomId();

            var product = _mapper.Map<Product>(productDto);


            while (await _uof.StoreRepository.GetProductByIdAsync(product.Id) != null)
            {
                product.Id = GeneratorId.CreateRandomId();
            }

            store.Products.Add(product);


            if (await _uof.Complete())
            {
                productDto.Id = product.Id;
                return Ok(new { id = productDto.Id });
            } 

            return BadRequest("Field to product");
        }


        [HttpPost("add-product-photos")]
        public async Task<IActionResult> AddProductPhotos([FromForm]IFormFile[] files,string id)
        {
            var product = await _uof.StoreRepository.GetProductByIdAsync(id);
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


        [HttpGet("searchProducts")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> searchProducts(string searchKey)
        {

            var user = await _userManager.GetUserAsync(User);

            var store = await _uof.StoreRepository.GetStoreAsync(user);

            var products = await _uof.StoreRepository.GetProductsAsync(store);

            var productsToRetuen = products.Where(p => p.Id.ToLower().Contains(searchKey.ToLower().Trim()) || p.Name.ToLower().Contains(searchKey.ToLower().Trim()))
                .Select(p => _mapper.Map<ProductDto>(p))
                .ToList();

            return productsToRetuen;

        }





        [HttpPut("make-photo-main")]
        public async Task<IActionResult> MakePhotoMain(string productId,int? id)
        {
            if (productId == null) return BadRequest("product id is null!!");
            if (id == null) return BadRequest();

            var product = await _uof.StoreRepository.GetProductByIdAsync(productId);

            var photo = product.Photos.FirstOrDefault(ph => ph.Id == id);

            if (photo.IsMain) return BadRequest("This photo is already main!!");

            foreach(var productPhoto in product.Photos)
            {
                productPhoto.IsMain = false;
            }

            photo.IsMain = true;

            if (!await _uof.Complete()) return BadRequest("Field to change main photo!!");

            return Ok();
            
        }


        [HttpPut("edit-product")]
        public async Task<IActionResult> EditProduct([FromForm] IFormFile[] updatedFiles,
                                                      [FromForm] IFormFile[] addedFiles,
                                                      [FromForm]string[] updated,
                                                      [FromForm] string[] deleted,
                                                      [FromQuery] ProductDto productDto)
        {
            //Edit product imformations

            var product = await _uof.StoreRepository.GetProductByIdAsync(productDto.Id);

            _mapper.Map(productDto, product);

            _uof.StoreRepository.UpdateProduct(product);

            if (!await _uof.Complete()) BadRequest("Field to update product!!");



            //Update photo
            if (updated != null)
            {
                for (int i = 0; i< updated.Length; i++)
                {
                    string publicId = updated[i];

                    var deleteResult = await _phtotoServices.RemovePhoto(publicId);

                    if (deleteResult.Error != null) return BadRequest(deleteResult.Error.Message);

                    var uploadResult = await _phtotoServices.AddProductPhotoAsync(updatedFiles[i]);

                    if (uploadResult.Error != null) return BadRequest(uploadResult.Error.Message);

                    var photo = product.Photos.FirstOrDefault(ph => ph.PublicId == publicId);

                    photo.Url = uploadResult.Url.AbsoluteUri;
                    photo.PublicId = uploadResult.PublicId;

                    if (!await _uof.Complete()) return BadRequest("Field to update photo!!");

                }
            }



            //Add photo
            if (addedFiles.Length > 0)
            {
                foreach(var added in addedFiles)
                {
                    var uploadResult = await _phtotoServices.AddProductPhotoAsync(added);

                    if (uploadResult.Error != null) return BadRequest(uploadResult.Error.Message);

                    var productPhoto = new ProductPhoto
                    {
                        Url = uploadResult.Url.AbsoluteUri,
                        PublicId = uploadResult.PublicId
                    };

                    product.Photos.Add(productPhoto);

                    if (!await _uof.Complete()) return BadRequest("Field to add photo!");

                }
            }






            //Delete photo
            if (deleted != null)
            {
                foreach(string pId in deleted)
                {
                    var deleteResult = await _phtotoServices.RemovePhoto(pId);

                    if (deleteResult.Error != null) return BadRequest(deleteResult.Error.Message);

                    var photo = product.Photos.FirstOrDefault(ph => ph.PublicId == pId);

                    product.Photos.Remove(photo);

                    if (!await _uof.Complete()) return BadRequest("Filed to delete photo!!");
                }
            }


            return Ok();
        }



        [HttpDelete("delete-product")]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            var product = await _uof.StoreRepository.GetProductByIdAsync(productId);

            if (product == null) return NotFound("Id is not correct");

            //Remove product photos
            foreach(var photo in product.Photos)
            {
                var deleteResult = await _phtotoServices.RemovePhoto(photo.PublicId);

                if (deleteResult.Error != null) return BadRequest(deleteResult.Error.Message);

            }

            _uof.StoreRepository.RemoveProduct(product);

            if (!await _uof.Complete()) return BadRequest("Field to Remove the product");

            return Ok();
        }

    }
}
