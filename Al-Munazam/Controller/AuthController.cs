using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Application.DTOs;
using Application.IRepositories;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Entities;
using Application.DTOs.User;

namespace Al_Munazam.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IModuleRepository _module;
        private readonly IMapper _mapper;   

        public AuthController(IUserRepository userRepository, IConfiguration configuration, IModuleRepository module, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _module = module;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginModel)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginModel.email);
            // Decrypt the password
            string decryptedPassword;
            try
            {
                decryptedPassword = null;
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid encrypted password format.");
            }
            if (user == null || !BCrypt.Net.BCrypt.Verify(decryptedPassword, user[0].password))
            {
                return Unauthorized("Invalid email or password.");

            }
            // Load user modules if not already loaded
            if (user[0].modules == null || !user[0].modules.Any())
            {
                user[0].modules = _mapper.Map<List<ModuleDto>>(await _module.GetAllModulesAsync(user[0]._id.ToString()));
            }

            // Generate JWT token
            var token = GenerateJwtToken(user[0]);
           await _userRepository.AddRefreshTokenAt(loginModel.email);
            return Ok(new
            {
                accessToken = token,
                refreshToken = token,
                data = new { user=user[0] }
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto user)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(user.email);
            if (existingUser != null)
            {
                return BadRequest("User already exists.");
            }
            await _userRepository.AddUserAsync(user);

            return Ok(new { Message = "User registered successfully." });
        }
        
        [HttpGet("token")]
        public async Task<IActionResult> GetUserFromToken()
        {
            // Extract token from the Authorization header
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Authorization token is missing.");
            }

            // Parse the token and extract the email
            var email = GetEmailFromJwt(token);

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("No email claim found in the JWT.");
            }
            var user = await _userRepository.GetUserByEmailAsync(email);

            
            return Ok(new { data = new { user } });
        }

        private string GetEmailFromJwt(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();

            // Read the token (this doesn't validate it, just parses it)
            var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

            if (jsonToken != null)
            {
                // Try to get the email claim
                var emailClaim = jsonToken?.Claims?.FirstOrDefault(c => c.Type == "sub")?.Value;
                return emailClaim;
            }

            return null;
        }

        #region PrivateMethods

        private string GenerateJwtToken(UserDto user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.type)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}
