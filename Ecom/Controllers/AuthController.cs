using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Ecommerce.DTOs.Authentication;
using Ecommerce.Model;
using Ecommerce.Static;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]

    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;


        public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
            this.configuration = configuration;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationDTO userDTO)
        {
            logger.LogInformation($"Registration Attempt for {userDTO.Email}");
            try
            {
                var user = mapper.Map<ApplicationUser>(userDTO);
                user.UserName = userDTO.Email;
                var result = await userManager.CreateAsync(user, userDTO.Password);

                if (!result.Succeeded == false)
                {
                    foreach (var error in result.Errors)
                    {
                        logger.LogInformation($"Registration Failed {userDTO.Email} {error.Description}");
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    return BadRequest(ModelState);
                }


                await userManager.AddToRoleAsync(user, "User");
                logger.LogInformation($"Registred Successfully {userDTO.Email}");
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something went Wrong in the {nameof(Register)}");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);

            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginUserDTO loginUserDTO)
        {
            logger.LogInformation($"login Attempt for {loginUserDTO.Email}");
            try
            {
                var user = await userManager.FindByEmailAsync(loginUserDTO.Email);
                var passwordValid = await userManager.CheckPasswordAsync(user, loginUserDTO.Password);

                if (user == null || passwordValid == false)
                {
                    return Unauthorized(loginUserDTO);
                }

                string tokenString = await GenerateToken(user);
                var response = new AuthResponse
                {
                    Email = loginUserDTO.Email,
                    Token = tokenString,
                    UserID = user.Id,
                    //FirstName=user.FirstName,

                };
                logger.LogInformation($"Logined Successfully {loginUserDTO.Email}");

                return Ok(response);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<string> GenerateToken(ApplicationUser user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q));
            var userClaims = await userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
       {
           new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
           new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
           new Claim(JwtRegisteredClaimNames.Email,user.Email),
           //new Claim(JwtRegisteredClaimNames.GivenName,user.FirstName),
           new Claim(CustomClaimTypes.uid,user.Id),
           new Claim(CustomClaimTypes.name,user.FirstName),
       }
         .Union(roleClaims)
         .Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToInt32(configuration["JwtSettings:Duration"])),
                signingCredentials: credentials

                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
