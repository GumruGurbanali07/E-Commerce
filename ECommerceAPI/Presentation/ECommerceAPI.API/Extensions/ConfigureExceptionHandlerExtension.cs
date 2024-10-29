using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace ECommerceAPI.API.Extensions
{
	public static class ConfigureExceptionHandlerExtension
	{
		public static void ConfigureExceptionHandler<T>(this WebApplication webApplication, ILogger<T> logger)
		{
			webApplication.UseExceptionHandler(builder =>
			{
				builder.Run(async context =>
				{
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					context.Response.ContentType = MediaTypeNames.Application.Json;
					var contextFeatures = context.Features.Get<IExceptionHandlerFeature>();
					if (contextFeatures != null)
					{
						logger.LogError(contextFeatures.Error.Message);
						await context.Response.WriteAsync(JsonSerializer.Serialize(new
						{
							StatusCode = context.Response.StatusCode,
							Message = contextFeatures.Error.Message,
							Title = "Error founded!"

						}));
					}

				});
			});
		}



	}
}
