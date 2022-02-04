using AutoMapper;
using AutoMapper.QueryableExtensions;
using everest.Entities;
using everest.Extensions;
using everest.Helpers;
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
        private readonly IMapper _mapper;

        public UsersController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
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
    }
}
