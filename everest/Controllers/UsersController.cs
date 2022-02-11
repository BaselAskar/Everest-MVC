using AutoMapper;
using AutoMapper.QueryableExtensions;
using everest.DTOs;
using everest.Entities;
using everest.Extensions;
using everest.Helpers;
using everest.Interfaces;
using everest.ModelsView;
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
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UsersController(UserManager<AppUser> userManager, IUnitOfWork uow, IMapper mapper)
        {
            _userManager = userManager;
            _uow = uow;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<PageList<UserView>> GetUsers([FromQuery] UserParams userParams)
        {
            var admin = await _userManager.GetUserAsync(User);

            var query = _userManager.Users
                .Where(user => user.Id != admin.Id)
                .OrderByDescending(user => user.Created)
                .AsQueryable();


            if (userParams.Search != null)
            {
                query = query.Where(user => user.UserName.ToLower().Contains(userParams.Search.ToLower()));
            }

            var usersQuery = query.Select(user => _mapper.Map<UserView>(user).UpdateRoleName());


            var result = await PageList<UserView>.CreateAsync(usersQuery,userParams.PageNumber,userParams.PageSize);

            Response.AddPaginationHeader(userParams.PageNumber, userParams.PageSize, result.TotalCount, result.TotalPages);

            return result;

        }



        [HttpPost("change-role")]
        public async Task<IActionResult> ChangeRole(string userId,string role,ClassificationDto[] classifications)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest("هذا المستخدم غير موجود");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Count > 1 && userRoles.Contains(role) && role != "Customer")
            {
                return BadRequest("خطا في صفة المستخدم");
            }


            if (role == "Store")
            {

                var store = await _uow.StoreRepository.GetStoreWithClassificationsAsync(user);

                if (store == null)
                {
                    _uow.StoreRepository.AddStore(new Store { User = user, UserId = user.Id });

                    if (!await _uow.Complete()) return BadRequest("Something was wrong during add store!");

                    store = await _uow.StoreRepository.GetStoreWithClassificationsAsync(user);

                    foreach (var classificationDto in classifications)
                    {
                        var classification = await _uow.StoreClassificationRepository.GetStoreClassificationByTitleAndNameAsync(classificationDto);

                        store.Classifications.Add(classification);

                        if (!await _uow.Complete()) return BadRequest("Something was wrong during add classification");
                    }

                }
                else
                {
                    store.Valid = true;

                    if (!await _uow.Complete()) return BadRequest("Somthing was worng during editing store");
                }


                await _userManager.AddToRoleAsync(user, role);

                return Ok();
            }


            if (role == "Customer")
            {

                var store = await _uow.StoreRepository.GetStoreAsync(user);

                if (store != null)
                {
                    store.Valid = false;

                    if (!await _uow.Complete()) return BadRequest("Something was wrong during edit store");
                }

                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRoleAsync(user, role);

                return Ok();
            }


            return BadRequest("role is not correct!");
        }
    }
}
