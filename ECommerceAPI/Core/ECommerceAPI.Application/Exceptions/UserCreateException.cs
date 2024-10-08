using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Exceptions
{
	public class UserCreateException: Exception
	{
        public UserCreateException()
        {
            
        }

		public UserCreateException(string? message) : base(message)
		{
		}

		public UserCreateException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
