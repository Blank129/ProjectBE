using DataAccess.EShop.DataRequests;
using DataAccess.EShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.IServices
{
	public interface IRoleRepository
	{
		Task<List<Role>> GetListRole();
		Task<ReturnData> CreateRole(CreateRole role);
		Task<ReturnData> UpdateRole(int roleId, CreateRole role);
		Task<ReturnData> Role_Delete(int roleId);
	}
}
