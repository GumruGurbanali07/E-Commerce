﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities.Identity
{
	public class AppUser :IdentityUser<string>
	{
		public AppUser()
		{
			Id = Guid.NewGuid().ToString(); 
		}
		public string Name { get; set; }
		public string? RefreshToken { get; set; }
		public DateTime? RefreshTokenEndDate { get; set; }
		public ICollection<Basket> Basket { get; set; }
	}
}
