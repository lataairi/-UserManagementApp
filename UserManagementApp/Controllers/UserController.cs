using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementApp.Data;
using UserManagementApp.Models;

namespace UserManagementApp.Controllers
{
    [Route("api/UserController")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Ctor
        private readonly UserDbContext _context;
        public UserController(UserDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Api Endpoint

        /// <summary>
        /// Get the all user record
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _context.User.ToListAsync();
        }

        /// <summary>
        /// get the single user based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<User> GetById(int id)
        {
            if (id != 0)
            {
                return await _context.User.FindAsync(id);
            }
            return null;
        }

        /// <summary>
        /// Create the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                if (user != null)
                {
                    await _context.User.AddAsync(user);
                    await _context.SaveChangesAsync();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Update user record
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> put([FromBody] User user)
        {
            try
            {
                if (user != null)
                {
                    _context.Entry(user).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {               
                    var user = await _context.User.FindAsync(id);
                if (user != null)
                {
                    _context.User.Remove(user);
                    _context.SaveChanges();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpPatch]
        //public async Task<IActionResult> patch(int id)
        //{
        //    try
        //    {
        //        if (id == 0)
        //        {
        //            return BadRequest();
        //        }
        //        var user = _context.User.Find(id);
        //        await _context.SaveChangesAsync();
        //        return Ok(user);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
        #endregion
    }
}
