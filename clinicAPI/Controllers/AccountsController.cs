using AutoMapper;
using clinicAPI.DTOs;
using clinicAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace clinicAPI.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController: ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AccountsController(Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet("listUsers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isClient")]
        public async Task<ActionResult<List<UserDTO>>> GetListUSers([FromQuery]
        PaginationDTO paginationDTO)
        {
            var queryable = context.Users.AsQueryable();
            await HttpContext.InsertParameterPaginationInHeader(queryable);
            var users = await queryable.OrderBy(x => x.Email).Paginate(paginationDTO).ToListAsync();
            return mapper.Map<List<UserDTO>>(users);
        }


        [HttpPost("makeClient")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isClient")]
        public async Task<ActionResult> MakeClient([FromBody] string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            await userManager.AddClaimAsync(user, new Claim("role", "client"));
            return NoContent();
        }


        [HttpPost("removeClient")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isClient")]
        public async Task<ActionResult> RemoveClient([FromBody] string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            await userManager.RemoveClaimAsync(user, new Claim("role", "client"));
            return NoContent();
        }

        [HttpPost("create")]
        public async Task<ActionResult<AuthenticationResponse>> Create(
            [FromBody] UserCredentials userCredentials)
        {
            var user = new IdentityUser { UserName = userCredentials.Email, Email = userCredentials.Email };
            var result = await userManager.CreateAsync(user, userCredentials.Password);
           if (result.Succeeded)
           {
                return BuildToken(userCredentials);
           }
           else
           {
                return BadRequest(result.Errors);
           }
        }


        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(
            [FromBody] UserCredentials userCredentials)
        {
            var result = await signInManager.PasswordSignInAsync(userCredentials.Email,
                userCredentials.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return BuildToken(userCredentials);
            }
            else
            {
                return BadRequest("Incorect Login");
            }
        }
         
        private AuthenticationResponse BuildToken(UserCredentials userCredentials)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", userCredentials.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(10);

            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);
            return new AuthenticationResponse() {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
