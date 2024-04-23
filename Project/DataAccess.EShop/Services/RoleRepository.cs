using DataAccess.EShop.DataRequests;
using DataAccess.EShop.Entities;
using DataAccess.EShop.EntitiesFramework;
using DataAccess.EShop.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.Services
{
	public class RoleRepository : IRoleRepository
	{	
		private EShopDBContext _context;

		public RoleRepository(EShopDBContext context)
		{
			_context = context;
		}

		public async Task<List<Role>> GetListRole()
		{
			var list = new List<Role>();
			try
			{

				list = _context.Role.ToList();

			}
			catch (Exception ex)
			{
				throw ex;
			}
			return list;
		}

		public async Task<ReturnData> CreateRole(CreateRole role)
		{
			var returnData = new ReturnData();
			try
			{
				if (role == null)
				{
					returnData.returnCode = -1;
					returnData.returnMessage = "Dữ liệu đầu vào không hợp lệ";
					return returnData;
				}
				else
				{
					var roleEntity = new Role();
					roleEntity.RoleName = role.RoleName;

					_context.Role.Add(roleEntity);
					_context.SaveChanges();
				}
				return returnData;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public async Task<ReturnData> UpdateRole(int roleId, CreateRole role)
		{
			var returnData = new ReturnData();
			try
			{
				if (roleId <= 0)
				{
					returnData.returnCode = -1;
					returnData.returnMessage = "Id role không hợp lệ";
					return returnData;
				}

				if (role == null)
				{
					returnData.returnCode = -1;
					returnData.returnMessage = "Dữ liệu đầu vào không hợp lệ";
					return returnData;
				}

				else
				{
					var selectedRole = _context.Role.SingleOrDefault(b => b.Id == roleId);

					if (selectedRole == null)
					{
						returnData.returnCode = -2;
						returnData.returnMessage = "Không tồn tại role cần sửa";
						return returnData;
					}

					selectedRole.RoleName = role.RoleName;

					_context.SaveChanges();
				}
				return returnData;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public async Task<ReturnData> Role_Delete(int roleId)
		{
			var returnData = new ReturnData();
			try
			{
				// step 1 : Kiểm tra đầu vào hợp lệ 

				if (roleId <= 0)
				{
					returnData.returnCode = -1;
					returnData.returnMessage = "Dữ liệu đầu vào không hợp lệ";
					return returnData;
				}

				// step 2 : Kiểm tra dữ liệu tồn tại không

				var role = _context.Role.Where(s => s.Id == roleId).FirstOrDefault();

				if (role == null || role.Id <= 0)
				{
					returnData.returnCode = -2;
					returnData.returnMessage = "Không tồn tại role cần xóa";
					return returnData;
				}


				// step 3 : Xóa

				_context.Role.Remove(role);

				var result = _context.SaveChanges();

				if (result < 0)
				{
					returnData.returnCode = -11;
					returnData.returnMessage = "Xóa role thất bại";
					return returnData;
				}


				returnData.returnCode = 1;
				returnData.returnMessage = "Xóa role thành công";
				return returnData;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}
	}
}