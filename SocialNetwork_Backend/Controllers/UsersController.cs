using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetwork_Backend.BDContext;
using SocialNetwork_Backend.Filters;
using SocialNetwork_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialNetwork_Backend.Controllers
{
    [Route("api/[controller]/[action]")]
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
        [HttpGet("page={page}/count={count}", Name = "GetUserPage")]
        public IActionResult GetUserPage(int page, int count)
        {
            try
            {
                int number = _userContext.Users.Count();
                var db = _userContext.Users.Skip(page * count).Take(count)
                    .Select(u => new
                    {
                        id = u.Id,
                        name = u.Name,
                        photoUrl = u.PhotoUrl,
                        status = u.status,
                        followed = u.followed
                    });
                if (db == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    items = db,
                    number = number
                });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet("id={id}", Name = "GetUserProfile")]
        public IActionResult GetUserProfile(int id)
        {
            try
            {
                var user = _userContext.Users
                    .Where(u => u.Id == id)
                    .Select(p => p.Profile)
                    .Select(n => new
                    {
                        userId = n.User.Id,
                        lookingForAJob = n.LookingForAJob,
                        lookingForAJobDescription = n.LookingForAJobDescription,
                        contacts = n.Contacts,
                        photoUrl = n.User.PhotoUrl,
                        name = n.User.Name
                    }).FirstOrDefault();
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet(Name = "GetMe")]
        [JwtAuthentication]
        public IActionResult GetMe()
        {
            try
            {
                var user = User.Identities.FirstOrDefault();
                if (user.IsAuthenticated)
                {
                    return Ok(new
                    {
                        auth = true,
                        data = new
                        {
                            username = user.Claims
                            .Where(c => c.Type == ClaimTypes.Name)
                            .Select(c => c.Value)
                            .SingleOrDefault(),
                            id = user.Claims
                            .Where(t => t.Type == ClaimTypes.SerialNumber)
                            .Select(s => s.Value)
                            .SingleOrDefault()
                        }
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        auth = false
                    });
                }
            }catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
