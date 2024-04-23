using DataAccess.Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using DataAccess.EShop.DataRequests;
using DataAccess.EShop.Entities;

namespace DataAccess.Blog.IServices
{
	public interface IUserRepository
	{
		Task<List<User>> GetListUser();
		Task<ReturnData> CreateUser(CreateUser user);
		Task<ReturnData> UpdateUser(int userId, CreateUser user);
		Task<ReturnData> User_Delete(int userId);
		Task<User> Login(UserLoginRequestData requestData);
		Task<int> UpdateRefeshToken(UserUpdateRefeshTokenRequestData requestData);
		Task<Function> GetFunctionByCode(string functionCode);
		Task<UserFunction> UserFunction_GetRole(int userId, int functionId);
	}
}