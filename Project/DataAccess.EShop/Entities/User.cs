﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Blog.Entities
{
	public class User
	{
		public int Id { get; set; }

		[Required][StringLength(50)] public string Username { get; set; }

		[Required][StringLength(100)] public string Password { get; set; }


		[Required] public int roleId { get; set; }

		[Required][StringLength(100)] public string RefreshToken { get; set; }

		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime RefreshTokenExpiredDate { get; set; }
	}
}