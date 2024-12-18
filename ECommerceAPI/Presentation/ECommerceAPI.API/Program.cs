using ECommerceAPI.API.Configurations.ColumnWriters;
using ECommerceAPI.API.Extensions;
using ECommerceAPI.Application;
using ECommerceAPI.Application.Validator.Products;
using ECommerceAPI.Infrastructure;
using ECommerceAPI.Infrastructure.Filters;
using ECommerceAPI.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using System.Text;
using ECommerceAPI.SiganlR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationService();
builder.Services.AddSignalRServices();


//builder.Services.AddCors(options=>options.AddDefaultPolicy(policy =>policy.WithOrigins("http://localhost:7191", "https://localhost:7191").AllowAnyHeader().AllowAnyMethod()));

Logger log = new LoggerConfiguration()
	.WriteTo.Console()
	.WriteTo.File("logs/log.txt")
	.WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "logs", needAutoCreateTable: true,
	columnOptions: new Dictionary<string, ColumnWriterBase>
	{
		{"message",new RenderedMessageColumnWriter() },
		{"message_template", new MessageTemplateColumnWriter() },
		{"level",new LevelColumnWriter()},
		{"time_stamp", new TimestampColumnWriter()},
		{"exception",new ExceptionColumnWriter()},
		{"log_event", new LogEventSerializedColumnWriter()},
		{"user_name",new UsernameColumnWriter()}
	})
	.Enrich.FromLogContext()
	.MinimumLevel.Information()
	.CreateLogger();
builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
	logging.LoggingFields = HttpLoggingFields.All;
	logging.RequestHeaders.Add("sec-ch-ua");
	logging.ResponseHeaders.Add("MyResponseHeader");
	logging.MediaTypeOptions.AddText("application/javascript");
	logging.RequestBodyLogLimit = 4096;
	logging.ResponseBodyLogLimit = 4096;
	logging.CombineLogs = true;
});


builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
	.AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
	.ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer("Admin", options =>
	{
		options.TokenValidationParameters = new()
		{
			ValidateAudience = true,
			ValidateIssuer = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidAudience = builder.Configuration["Token:Audience"],
			ValidIssuer = builder.Configuration["Token:Issuer"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
			LifetimeValidator = (notBefore, expires, securityToken, validationParamters) => expires != null ? expires > DateTime.UtcNow : false,
			NameClaimType = ClaimTypes.Name

		};
	});



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}


app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseStaticFiles();

app.UseSerilogRequestLogging();//bundan sonraki middleware-ler loglanir
app.UseHttpLogging();

app.UseCors();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
	var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
	LogContext.PushProperty("user_name", username);
	await next();
});


app.MapControllers();

app.MapHubs();

app.Run();
