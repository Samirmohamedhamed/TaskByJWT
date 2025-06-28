using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WepApiDemo.Dto;
using WepApiDemo.Model;

namespace WepApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountsController(UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }
        //Register Action
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegistrationDto registrationDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationDto.UserName,
                Email = registrationDto.Email,
                
            };
          var result = await  userManager.CreateAsync(user, registrationDto.Password);
            if (result.Succeeded) {
                return Ok("Account Add Success");
            }
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(errors);
        }
        //Login Action
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LogInDto loginDto)
        {

           var user =  await userManager.FindByNameAsync(loginDto.Name);
           if(user is not null)
            {
                var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
                if(result)
                {
                    //Make Claims Token
                    var claims = new List<Claim>();//Claim has a Type and Value
                    claims.Add(new Claim(ClaimTypes.Name, user.UserName));//Name of user in security
                    claims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id));// Id of user in security
                    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));//id of token in jwt must be unique
                    //get role
                    var roles = await userManager.GetRolesAsync(user); //role Of User
                    foreach(var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role,role));

                    }
                    SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));//Convert Key to bytes 
                    SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);//Key And Algorithm
                    //Create Token
                    JwtSecurityToken mytoken = new JwtSecurityToken( // JwtSecurityToken design Token
                        issuer: configuration["JWT:ValidIssuer"],//Url Wep Api Provider
                        audience: configuration["JWT:ValidAudiance"],//URL Consumer
                        claims: claims,
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: signingCredentials//Key to make signture to token

                        );
                    return Ok(new
                    {
                        toke = new JwtSecurityTokenHandler().WriteToken(mytoken),
                        expiration = mytoken.ValidTo,
                    });
                }
                return BadRequest();
            }
            return NotFound();

        }



    }
}
