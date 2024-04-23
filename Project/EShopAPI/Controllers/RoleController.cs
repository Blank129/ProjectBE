using DataAccess.EShop.DataRequests;
using DataAccess.EShop.Entities;
using DataAccess.EShop.UnitOfWork;
using EShopAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace EShopAPI.Controllers
{
	[Route("roles")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private IEShopUnitOfWork _unitOfWork;
		private IConfiguration _configuration;
		public RoleController(IEShopUnitOfWork eShopUnitOfWord, IConfiguration configuration)
		{
			_unitOfWork = eShopUnitOfWord;
			_configuration = configuration;
		}


		[HttpGet()]
		[EShopAuthorize("R1", "Get")]
		public async Task<ActionResult> Role_Getlist()
		{

			var list = new List<Role>();
	
			list = await _unitOfWork._roleRepository.GetListRole();
			
			return Ok(list);
		}

		[HttpPost]
		[EShopAuthorize("R2", "Post")]
		public async Task<ActionResult> Post_Create([FromBody] CreateRole role)
		{
			try
			{
				var returnData = new ReturnData();
				if (role == null)
				{
					return BadRequest();
				}

				returnData = await _unitOfWork._roleRepository.CreateRole(role);

				return StatusCode(201);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPut("{role_id:int}")]
		[EShopAuthorize("R3", "Put")]
		public async Task<ActionResult> Post_Update(int role_id, [FromBody] CreateRole role)
		{
			var returnData = new ReturnData();
			returnData = await _unitOfWork._roleRepository.UpdateRole(role_id, role);
			return Ok(returnData);
		}


		[HttpDelete("{post_id:int}")]
		[EShopAuthorize("R4", "Delete")]
		public async Task<ActionResult> Post_Delete(int role_id)
		{
			var returnData = new ReturnData();
			returnData = await _unitOfWork._roleRepository.Role_Delete(role_id);
			return Ok(returnData);
		}
	}
}
