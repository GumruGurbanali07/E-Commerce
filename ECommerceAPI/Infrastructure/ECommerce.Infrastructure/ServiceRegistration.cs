using ECommerceAPI.Application.Abstraction.Services;
using ECommerceAPI.Application.Abstraction.Token;
using ECommerceAPI.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure
{
	public static class ServiceRegistration
	{
		public static  void AddInfrastructureServices(this IServiceCollection services)
		{
			services.AddScoped<ITokenHandler, ECommerceAPI.Infrastructure.Services.Token.TokenHandler>();
			services.AddScoped<IMailService,MailService>();
		}

	}

}
