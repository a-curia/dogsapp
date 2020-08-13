using DogsApp.Models;
using DogsApp.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DogsApp.Controllers
{
    public class IdentityController : ApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationSettings applicationSettings;

        public IdentityController(UserManager<User> userManager, IOptions<ApplicationSettings> applicationSettings)
        {
            this._userManager = userManager;
            this.applicationSettings = applicationSettings.Value;
        }

        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {

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

        [Route(nameof(Login))]
        public async Task<ActionResult<string>> Login(LoginRequestModel model)
        {

            var user = await this._userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return this.Unauthorized();
            }

            var passwordValid = await this._userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.applicationSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;

        }

    }
}