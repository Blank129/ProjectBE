using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.Entities
{
	public class Role
	{
		public int Id { get; set; }
		[Required][StringLength(50)] public string RoleName { get; set; }
	}
}
