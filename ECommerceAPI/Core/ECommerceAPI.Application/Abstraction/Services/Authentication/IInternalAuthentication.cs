using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.DTOs;
namespace ECommerceAPI.Application.Abstraction.Services.Authentication
{
	public interface IInternalAuthentication
	{
		Task<ECommerceAPI.Application.DTOs.Token> LoginAsync(string UsernameOrEmail, string  password, int tokeLifeTime);
	}
}
