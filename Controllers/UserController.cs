using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Microsoft.IdentityModel.Tokens;


namespace Perficient.Training.JwtAuthentication
{


    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        
        [Authorize (Roles = "Reader, Contributor, Manager")]
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<User>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _userService.GetUsersAsync();
            /*
            var role = User.FindFirst("Role");


            if (role == null)
            {
                return Unauthorized("Rol Null");
            }
            else if (role.Value.ToString() == "Reader" || role.Value.ToString() == "Manager" || role.Value.ToString() == "Contributor")
            {
                return Ok(users);
            }
            else
            {
                return Unauthorized("You shall not pass");
            }
            */

            return Ok(users);


        }


        [Authorize (Roles = "Reader, Contributor, Manager")]
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUserAsync([FromRoute] Guid id)
        {
            var user = await _userService.GetUserAsync(id);
            /*
            var role = User.FindFirst("Role");

            if (role == null)
            {
                return Unauthorized("Rol Null");
            }
            else if (role.Value.ToString() == "Reader" || role.Value.ToString() == "Contributor" || role.Value.ToString() == "Manager")
            {
                return user is not null ? Ok(user) : NotFound();
            }
            else
            {
                return Unauthorized("You shall not pass");
            }
            */
            return user is not null ? Ok(user) : NotFound();
            
        }


        [Authorize (Roles = "Contributor, Manager")]
        [HttpPost]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserDto user)
        {
            /*
            var role = User.FindFirst("Role");

            if (role == null)
            {
                return Unauthorized("Rol Null");
            }
            else if (role.Value.ToString() == "Contributor" || role.Value.ToString() == "Manager")
            {
                var userCreated = await _userService.CreateUserAsync(user);
                return Created($"/users/{userCreated.Id}", userCreated);
            }
            else
            {
                return Unauthorized("You shall not pass");
            }
            */
            var userCreated = await _userService.CreateUserAsync(user);
            return Created($"/users/{userCreated.Id}", userCreated);
        }

        [Authorize (Roles = "Contributor, Manager")]
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid id, [FromBody] UserDto user)
        {
            /*
            var role = User.FindFirst("Role");

            if (role == null)
            {
                return Unauthorized("Rol Null");
            }
            else if (role.Value.ToString() == "Contributor" || role.Value.ToString() == "Manager")
            {
                await _userService.UpdateUserAsync(id, user);
                return Ok(user);
            }
            else
            {
                return Unauthorized("You shall not pass");
            }
            */

            await _userService.UpdateUserAsync(id, user);
            return Ok(user);


        }

        [Authorize (Roles = "Manager")]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid id)
        {
            /*
            var role = User.FindFirst("Role");

            if (role == null)
            {
                return Unauthorized("Rol Null");
            }
            else if (role.Value.ToString() == "Manager")
            {
                await _userService.DeleteUserAsync(id);
                return Ok();
            }
            else
            {
                return Unauthorized("You shall not pass");
            }   
            */
            await _userService.DeleteUserAsync(id);
            return Ok();

        }
    }
}
