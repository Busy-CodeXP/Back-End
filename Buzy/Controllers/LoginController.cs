using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Buzy.DataAccess;
using Buzy.DataAccess.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Buzy.Controllers
{
    [Route("api/login")]
    public class LoginController : Controller
    {

        readonly BusHelperContext db;
        readonly IConfiguration configuration;

        public LoginController(IConfiguration _configuration, BusHelperContext db)
        {
            this.db = db;
            configuration = _configuration;
        }

        
        [HttpPost]
        public IActionResult RequestToken([FromBody] TokenRequest request, Usuario usuario)
        {
            Usuario user = db.Usuarios
                    .FirstOrDefault(c => c.email == usuario.email && c.senha == usuario.senha);

            if (request.email == usuario.email && request.password == usuario.email)
            {
                var claims = new[]
                {
            new Claim(ClaimTypes.Name, request.email)
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "yourdomain.com",
                    audience: "yourdomain.com",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return BadRequest("Could not verify username and password");
        }
    }
}