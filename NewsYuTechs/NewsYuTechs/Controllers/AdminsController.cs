using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsYuTechs.BL;
using NewsYuTechs.DAL;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace NewsYuTechs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly UserManager<Admin> _userManager;
        private readonly IAdminManager _adminManager;
        private readonly IConfiguration _configuration;

        public AdminsController(UserManager<Admin> userManager, IConfiguration configuration,IAdminManager adminManager)
        {
            _userManager = userManager;
            _adminManager = adminManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<string>> Register(RegisterDTO registerDTO)
        {
            var existingUser = await _userManager.FindByNameAsync(registerDTO.Username!);
            if (existingUser != null)
            {
                return BadRequest("Username is already taken.");
            }

            var newUser = new Admin
            {
                Id = Guid.NewGuid().ToString(),
                UserName = registerDTO.Username,
                Name = registerDTO.Name,
            };

            var creationResult = await _userManager.CreateAsync(newUser, registerDTO.Password!);

            if (!creationResult.Succeeded)
            {
                return BadRequest(creationResult.Errors);
            }

            // Fetch the user from the database
            var createdUser = await _userManager.FindByNameAsync(newUser.UserName!);
            if (createdUser == null)
            {
                return BadRequest("User was not found after creation");
            }

            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, newUser.UserName!),
            };

            // Now add the claims
            var claimsResult = await _userManager.AddClaimsAsync(newUser, userClaims);
            if (!claimsResult.Succeeded)
            {
                // If adding the claims fails, delete the user to avoid orphaned users
                await _userManager.DeleteAsync(newUser);
                return BadRequest(claimsResult.Errors);
            }

            // Use AdminManager to add additional functionality

            return Ok(newUser);
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<TokenDTO>> Login(LoginDTO credintials)
        {
            var user = await _userManager.FindByNameAsync(credintials.Username!);
            if (user == null)
            {
                return BadRequest("User Not Found");
            }
            if (await _userManager.IsLockedOutAsync(user))
            {
                return BadRequest("Try Again Later, Your Profile is locked!");
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            bool isAuthenticated = await _userManager.CheckPasswordAsync(user, credintials.Password!);
            if (!isAuthenticated)
            {
                await _userManager.AccessFailedAsync(user);
                return Unauthorized("Wrong Username or Password");
            }
            var exp = DateTime.Now.AddMinutes(750);
            var secretKey = _configuration.GetValue<string>("SecretKey");
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("The secret key cannot be null or empty.");
            }
            var secretKeyBytes = Encoding.ASCII.GetBytes(secretKey);
            var Key = new SymmetricSecurityKey(secretKeyBytes);
            var methodGeneratingToken = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
            var jwt = new JwtSecurityToken(
                claims: userClaims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(120)
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(jwt);
            return new TokenDTO
            {
                Token = tokenString,
                ExpiryDate = exp,
                Username = user.UserName,
                User_id = user.Id,
            };
        }

        [HttpGet]
        public IActionResult GetAllAdmins()
        {
            var Admins = _adminManager.GetAllAdmins();
            return Ok(Admins);
        }

        [HttpGet("{id}")]
        public IActionResult GetAdminById(string id)
        {
            var Admin = _adminManager.GetAdminById(id);
            if(Admin == null)
            {
                return BadRequest("User not found");
            }
            return Ok(Admin);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin(string id)
        {
            AdminDTO? Admin = _adminManager.GetAdminById(id);
            if (Admin == null)
            {
                return NotFound("User not found");
            }
            _adminManager.DeleteAdmin(id);
            return Ok("Admin "+id+" has been deleted successfully");
        }

        [HttpPut("{id}")]
        public ActionResult<Admin> UpdateAdmin(string id, AdminDTO Admin)
        {

            if (id != Admin.Id)
            {
                return BadRequest();
            }
            _adminManager.UpdateAdmin(Admin);
            return NoContent();
        }
    }
}
