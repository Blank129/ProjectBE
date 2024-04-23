using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.DataRequests
{
	public class UserLoginRequestData
	{
		public string username { get; set; }
		public string password { get; set; }
	}

	public class UserUpdateRefeshTokenRequestData
	{
		public int Id { get; set; }
		public string RefreshToken { get; set; }

		public DateTime RefreshTokenExpiredDate { get; set; }
	}

	public class CreateUser
	{

		public string Username { get; set; }

		public string Password { get; set; }


		[Required] public int roleId { get; set; }
	}
}
