using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testmysql.Context;
using testmysql.Models;
using testmysql.Utilities;

namespace testmysql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private User_RepoContext _context;

        public UserController(User_RepoContext context)
        {
            _context = context;
        }

        // GET api/values
        [Route("all"), HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var result = await _context.CoreUser
                .Include(p => p.Account)
                .ToListAsync();
                return Ok(result);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Route("new"), HttpPost]
        public async Task<ActionResult> NewUser(CoreUser user)
        {
            try
            {
                foreach (var item in user.Account)
                {
                    item.HashedPassword = AuthUtilities.HashPassword(user.TempPassword);
                }

                _context.CoreUser.Add(user);
                await _context.SaveChangesAsync();
                return Ok("User Saved!");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("update/{id}"), HttpPut]
        public async Task<ActionResult> UpdateUser(CoreUser user, int id)
        {
            try
            {
                var result = _context.CoreUser.Find(id);
                result = user;
                _context.CoreUser.Update(result);
                await _context.SaveChangesAsync();

                return Ok($"Updated record {user.Id} successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Autenticate"), HttpPost]
        public async Task<ActionResult> Authenticate(UserLogin user)
        {
            try
            {
                if (user != null)
                {
                    var result = await _context.Account.Where(p => p.EmailAddress == user.username).SingleOrDefaultAsync();
                    if (AuthUtilities.HashPassword(user.password) == result.HashedPassword)
                    {
                        result.Logins++;
                        _context.Account.Update(result);
                        await _context.SaveChangesAsync();
                        return Ok("You are authenticated!");
                    }
                    else
                    {
                        return BadRequest("You've entered the wrong username and or password.");
                    }
                }
                return BadRequest("Invalid request.");

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
