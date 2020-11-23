using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetwork_Backend.BDContext;
using SocialNetwork_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_Backend.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _userContext;
        public UsersController(UserContext context)
        {
            _userContext = context;
        }
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            
            var db = await _userContext.Users.FindAsync(id);
            if (db == null)
            {
                return NotFound();
            }
            return Ok(db);
        }
        [HttpGet(Name = "GetAllUsers")]
        public IActionResult GetAllUsers()
        {

            var db = _userContext.Users;

            if (db == null)
            {
                return NotFound();
            }

            return Ok(db);
        }

    }
}
