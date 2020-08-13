using DogsApp.Models;
using DogsApp.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DogsApp.Controllers
{
    public class IdentityController : ApiController
    {
        private readonly UserManager<User> _userManager;

        public IdentityController(UserManager<User> userManager)
        {
            this._userManager = userManager;
        }
        public async Task<IActionResult> Register(RegisterUserRequestModel model) {

            var user = new User
            {
                Email = model.Email,
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return this.StatusCode((int)HttpStatusCode.Created);
            }

            return BadRequest(result.Errors);
        }
    }
}
