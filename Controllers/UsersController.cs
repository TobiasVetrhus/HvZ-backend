using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HvZ_backend.Data.Entities;
using HvZ_backend.Services.Users;
using AutoMapper;
using HvZ_backend.Data.DTOs.Users;

namespace HvZ_backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(_mapper.Map<IEnumerable<UserDTO>>(await _userService.GetAllAsync()));
        }
        /*
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {

        }


        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {

        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {

        }
        */
    }
}
