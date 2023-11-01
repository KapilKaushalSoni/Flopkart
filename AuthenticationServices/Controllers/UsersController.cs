using AuthenticationServices.AuthenticationModels;
using AuthenticationServices.Models;
using AuthenticationServices.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationServices.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JwtSettings jwtSettings;
        private readonly IConfiguration configuration;

        public UsersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IOptions<JwtSettings> jwtSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.jwtSettings = jwtSettings.Value;
            this.configuration = configuration;
        }
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<APIResponse>> Create(UserViewModel userViewModel, CancellationToken cancellationToken)
        {
            APIResponse aPIResponse = new APIResponse();
            var res = await userManager.CreateAsync(new AppUser()
            {
                UserName = userViewModel.UserName,
                Email = userViewModel.Email,
                Mobile_Number = userViewModel.Mobile_Number
            }, userViewModel.Password);


            if (!res.Succeeded)
            {
                return Ok(new { Error = res.Errors.ToList() });
            }
            var oUser = await userManager.FindByNameAsync(userViewModel.UserName);
            //check roles not exists and create

            foreach (var role in userViewModel.Roles)
            {
                var isExists = await roleManager.RoleExistsAsync(role.Role);
                if (!isExists)
                {
                    await roleManager.CreateAsync(new IdentityRole() { Name = role.Role });
                }
                var isAlreadyInRole = await userManager.IsInRoleAsync(oUser, role.Role);
               
                if (!isAlreadyInRole)
                {
                    await userManager.AddToRoleAsync(oUser, role.Role);
                }
            }



            aPIResponse.Data = userViewModel;
            aPIResponse.Success = true;
            return Ok(aPIResponse);
        }
        [HttpGet]
        [Route("get/{userName}")]
        public async Task<ActionResult<APIResponse>> GetByUserName(string userName)
        {
            APIResponse aPIResponse = new APIResponse();
            var res = await userManager.FindByNameAsync(userName);
            aPIResponse.Data = res;
            aPIResponse.Success = true;
            return Ok(aPIResponse);
        }
        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult<APIResponse>> GetAllUsers()
        {
            APIResponse aPIResponse = new APIResponse();
            var res = await userManager.Users.ToListAsync();
            aPIResponse.Data = res;
            aPIResponse.Success = true;
            return Ok(aPIResponse);
        }
        [HttpPost]
        [Route("authenticate")]
        public async Task<ActionResult<LoginResponse>> Authenticate(LogInViewModel obj)
        {
            LoginResponse aPIResponse = new LoginResponse();

            // Validate User
            var oUser = await userManager.FindByNameAsync(obj.UserName);
            if (oUser == null)
                return NotFound(new { Message = "User not exists !!!" });

            var oValidUser = await signInManager.PasswordSignInAsync(oUser, obj.Password, false, false);

            // Validate Credentials
            if (!oValidUser.Succeeded)
                return Unauthorized(new { Message = "Invalid Credentials !!!" });

            //Get roles
            var roles = await userManager.GetRolesAsync(oUser);

            //Generate Jwt Token
            aPIResponse.Token = generateToken(oUser, roles);

            aPIResponse.UserName = obj.UserName.ToUpper();

            // Generate Refresh Token
            aPIResponse.RefreshToken = GenerateRefreshToken();

            return Ok(aPIResponse);
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private string generateToken(AppUser objUser, IList<string> roles)
        {
            List<Claim> lstClaims = new List<Claim>();
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(lstClaims);

            lstClaims.Add(new Claim(ClaimTypes.Name, objUser.UserName));

            foreach (var item in roles)
            {
                lstClaims.Add(new Claim(ClaimTypes.Role, item));
            }

            lstClaims.Add(new Claim(ClaimTypes.Email, objUser.Email));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(120),
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)), SecurityAlgorithms.HmacSha512Signature)

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}
