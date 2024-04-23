using DataAccess.Blog.Entities;
using DataAccess.Blog.IServices;
using DataAccess.EShop.DataRequests;
using DataAccess.EShop.Entities;
using DataAccess.EShop.EntitiesFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.Services
{
	public class UserRepository : IUserRepository
	{
		private EShopDBContext _context;
		public UserRepository(EShopDBContext context)
		{
			_context = context;
		}

		public async Task<List<User>> GetListUser()
		{
			var list = new List<User>();
			try
			{

				list = _context.User.ToList();

			}
			catch (Exception ex)
			{
				throw ex;
			}
			return list;
		}

		public async Task<ReturnData> CreateUser(CreateUser user)
		{
			var returnData = new ReturnData();
			try
			{
				if (user == null)
				{
					returnData.returnCode = -1;
					returnData.returnMessage = "Dữ liệu đầu vào không hợp lệ";
					return returnData;
				}
				else
				{
					var userEntity = new User();
					userEntity.Username = user.Username;
					userEntity.Password = user.Password;
					userEntity.roleId =  user.roleId;

					_context.User.Add(userEntity);
					_context.SaveChanges();
				}
				return returnData;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public async Task<ReturnData> UpdateUser(int userId, CreateUser user)
		{
			var returnData = new ReturnData();
			try
			{
				if (userId <= 0)
				{
					returnData.returnCode = -1;
					returnData.returnMessage = "Id user không hợp lệ";
					return returnData;
				}

				if (user == null)
				{
					returnData.returnCode = -1;
					returnData.returnMessage = "Dữ liệu đầu vào không hợp lệ";
					return returnData;
				}

				else
				{
					var selectedUser = _context.User.SingleOrDefault(b => b.Id == userId);

					if (selectedUser == null)
					{
						returnData.returnCode = -2;
						returnData.returnMessage = "Không tồn tại user cần sửa";
						return returnData;
					}

					selectedUser.Username = user.Username;
					selectedUser.Password = user.Password;
					selectedUser.roleId = user.roleId;

					_context.SaveChanges();
				}
				return returnData;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public async Task<ReturnData> User_Delete(int userId)
		{
			var returnData = new ReturnData();
			try
			{
				// step 1 : Kiểm tra đầu vào hợp lệ 

				if (userId <= 0)
				{
					returnData.returnCode = -1;
					returnData.returnMessage = "Dữ liệu đầu vào không hợp lệ";
					return returnData;
				}

				// step 2 : Kiểm tra dữ liệu tồn tại không

				var user = _context.User.Where(s => s.Id == userId).FirstOrDefault();

				if (user == null || user.Id <= 0)
				{
					returnData.returnCode = -2;
					returnData.returnMessage = "Không tồn tại user cần xóa";
					return returnData;
				}


				// step 3 : Xóa

				_context.User.Remove(user);

				var result = _context.SaveChanges();

				if (result < 0)
				{
					returnData.returnCode = -11;
					returnData.returnMessage = "Xóa sản phẩm thất bại";
					return returnData;
				}


				returnData.returnCode = 1;
				returnData.returnMessage = "Xóa sản phẩm thành công";
				return returnData;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public async Task<User> Login(UserLoginRequestData requestData)
		{
			var user = new User();
			try
			{
				user = _context.User.ToList().FirstOrDefault(user =>
					user.Username == requestData.username && user.Password == requestData.password);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return user;
		}
		public async Task<int> UpdateRefeshToken(UserUpdateRefeshTokenRequestData requestData)
		{
			try
			{
				var user = _context.User.ToList().FirstOrDefault(user => user.Id == requestData.Id);
				if (user != null)
				{
					user.RefreshToken = requestData.RefreshToken;
					user.RefreshTokenExpiredDate = requestData.RefreshTokenExpiredDate;
					_context.User.Update(user);
					_context.SaveChanges();

					return 1;
				}

				return 0;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<Function> GetFunctionByCode(string functionCode)
		{
			return _context.Function.FirstOrDefault(f => f.FunctionCode == functionCode);
		}
		public async Task<UserFunction> UserFunction_GetRole(int userId, int functionId)
		{
			return _context.UserFunction.FirstOrDefault(uf => uf.UserId == userId && uf.FunctionId == functionId);
		}
	}
}
