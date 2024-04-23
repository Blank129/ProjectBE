using DataAccess.Blog.Entities;
using DataAccess.EShop.DataRequests;
using DataAccess.EShop.Entities;
using DataAccess.EShop.UnitOfWork;
using EShopAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EShopAPI.Controllers
{
	[Route("admin")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private IEShopUnitOfWork _eShopUnitOfwork;
		private IConfiguration _configuration;

		private class UserResponse
		{
			public string username { get; set; }
			public string token { get; set; }
			public string refresh_token { get; set; }
		}

		public UserController(IEShopUnitOfWork eShopUnitOfwork, IConfiguration configuration)
		{
			_eShopUnitOfwork = eShopUnitOfwork;
			_configuration = configuration;
		}

		[HttpGet("users")]
		[EShopAuthorize("U1", "Get")]
		public async Task<ActionResult> User_Getlist()
		{

			var list = new List<User>();

			list = await _eShopUnitOfwork._userRepository.GetListUser();

			return Ok(list);
		}

		[HttpPost("users")]
		[EShopAuthorize("U2", "Post")]
		public async Task<ActionResult> User_Create([FromBody] CreateUser user)
		{
			try
			{
				var returnData = new ReturnData();
				if (user == null)
				{
					return BadRequest();
				}

				returnData = await _eShopUnitOfwork._userRepository.CreateUser(user);

				return StatusCode(201);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPut("users/{user_id:int}")]
		[EShopAuthorize("U3", "Put")]
		public async Task<ActionResult> Post_Update(int user_id, [FromBody] CreateUser user)
		{
			var returnData = new ReturnData();
			returnData = await _eShopUnitOfwork._userRepository.UpdateUser(user_id, user);
			return Ok(returnData);
		}


		[HttpDelete("users/{user_id:int}")]
		[EShopAuthorize("U4", "Delete")]
		public async Task<ActionResult> Post_Delete(int user_id)
		{
			var returnData = new ReturnData();
			returnData = await _eShopUnitOfwork._userRepository.User_Delete(user_id);
			return Ok(returnData);
		}

		[HttpPost("login")]
		public async Task<ActionResult> Login(UserLoginRequestData requestData)
		{
			try
			{
				if (requestData == null || requestData.username == null || requestData.password == null)
				{
					return BadRequest();
				}

				var user = await _eShopUnitOfwork._userRepository.Login(requestData);

				if (user == null || user.Id == null)
				{
					return Ok("User ko ton tai!");
				}

				var authClaims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.Username),
					new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				};

				var newAccessToken = CreateToken(authClaims);

				var token = new JwtSecurityTokenHandler().WriteToken(newAccessToken);
				var refreshToken = GenerateRefreshToken();

				var expriredDateSettingDay = _configuration["JWT:RefreshTokenValidityInDays"] ?? "";
				var userNewRefreshToken = new UserUpdateRefeshTokenRequestData
				{
					Id = user.Id,
					RefreshToken = refreshToken,
					RefreshTokenExpiredDate = DateTime.Now.AddDays(Convert.ToInt32(expriredDateSettingDay))
				};

				var update = await _eShopUnitOfwork._userRepository.UpdateRefeshToken(userNewRefreshToken);

				var response = new UserResponse();
				response.username = user.Username;
				response.token = token;
				response.refresh_token = refreshToken;

				return Ok(response);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		private static string GenerateRefreshToken()
		{
			var randomNumber = new byte[64];
			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}

		private JwtSecurityToken CreateToken(List<Claim> authClaims)
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
			_ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

			var token = new JwtSecurityToken(
				issuer: _configuration["JWT:ValidIssuer"],
				audience: _configuration["JWT:ValidAudience"],
				expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
			);

			return token;
		}
	}
}
