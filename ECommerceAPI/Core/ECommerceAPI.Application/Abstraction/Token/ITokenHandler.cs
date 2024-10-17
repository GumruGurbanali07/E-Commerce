using P=ECommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstraction.Token
{
	public interface ITokenHandler
	{
		P.Token CreateAccessToken(int minute);
		string CreateRefreshToken(int minute);	
	}
}
