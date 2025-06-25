using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using rilevazioniPresenza.Reps.UserFiles;
using rilevazioniPresenzaData.Models;
using rilevazioniPresenze.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace rilevazioniPresenze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public AuthController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("test")]
        [Authorize]
        public IActionResult test()
        {
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTOs LoginDTOs)
        {
            //if (user.Username == "admin" && user.Password == "password")
            //{
            //    var token = GenerateJwtToken(user.Username);
            //    return Ok(new { token });
            //}
            //return Unauthorized();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(LoginDTOs.Password);
            byte[] passwordHashed = MD5.HashData(passwordBytes);
            var user = _repo.GetUserByUsername(LoginDTOs.Username);
            if (user == null)
                return Unauthorized();
            if (Convert.ToHexString(passwordHashed).ToLower() != user.Password)
                return Unauthorized();
            var token = GenerateJwtToken(user.Username, user.Admin);

            return Ok(new { token });
        }

        private string GenerateJwtToken(string username, bool isAdmin)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(ClaimTypes.Role, isAdmin?"admin":"normal"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("questaèunachiavesegretapiùsicura123"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var claimsIdentity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Esempio: memorizza il cookie persistentemente
            };

            // 5. SignIn con l'identità e memorizzazione nel cookie
            //await HttpContext.SignInAsync(
            //    CookieAuthenticationDefaults.AuthenticationScheme,
            //    new ClaimsPrincipal(claimsIdentity),
            //    authProperties);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
