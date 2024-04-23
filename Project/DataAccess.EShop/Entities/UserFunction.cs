using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Blog.Entities
{
	public class UserFunction
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int FunctionId { get; set; }
		public int IsView { get; set; }
		public int IsCreate {get; set; }
		public int IsDelete { get; set; }
		public int isUpdate { get; set; }
	}
}
