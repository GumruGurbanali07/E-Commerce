﻿using ECommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
	public class LoginUserCommandResponse
	{


		public class LoginUserSuccessCommandResponse : LoginUserCommandResponse
		{
			public Token token { get; set; }
		}
		public class LoginUserErrorCommandResponse : LoginUserCommandResponse
		{
			public string Message;
		}
	}
}
