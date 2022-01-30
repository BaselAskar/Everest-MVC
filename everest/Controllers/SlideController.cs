using AutoMapper;
using everest.DTOs;
using everest.Entities;
using everest.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SlideController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPhotoServices _photoServices;
        private readonly IMapper _mapper;

        public SlideController(IUnitOfWork uof,UserManager<AppUser> userManager,IPhotoServices photoServices,IMapper mapper)
        {
            _uof = uof;
            _userManager = userManager;
            _photoServices = photoServices;
            _mapper = mapper;
        }


        [HttpGet("username-store")]
        public async Task<IActionResult> GetUserNameStore(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("The user has not found please try agin!");
            }

            return Ok(new { userName = user.UserName });
        }



        [HttpPost("add-slide")]
        public async Task<IActionResult> AddSlide([FromForm] IFormFile[] files,[FromForm] SlideForm slideForm)
        {
            if (files.Length == 0)
            {
                return BadRequest("You have to add images!!");
            }

            if (slideForm == null)
            {
                return BadRequest("you have to add informations!!");
            }

            var user = await _userManager.FindByIdAsync(slideForm.UserId);

            var slide = new Slide
            {
                Order = slideForm.Order,
                Speed = slideForm.Speed,
                Store = await _uof.StoreRepository.GetStoreAsync(user)
            };

            _uof.SlideRepository.AddSlide(slide);

            if (!await _uof.Complete()) return BadRequest("Something was wrong durign adding!!");

            var entitySlide = await _uof.SlideRepository.FindSlideByIdAsync(slide.Id);



            foreach (var image in files)
            {
                var uploadResult = await _photoServices.AddSlidePhotoAsync(image);

                if (uploadResult.Error != null) return BadRequest(uploadResult.Error.Message);

                var slidePhoto = new SlidePhoto
                {
                    Url = uploadResult.Url.AbsoluteUri,
                    PublicId = uploadResult.PublicId
                };

                entitySlide.Photos.Add(slidePhoto);

                if (!await _uof.Complete()) return BadRequest("Something was wrong durign adding!!");
            }


            return Ok();
        }
    }
}
