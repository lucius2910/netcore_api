using Application.Core.Extensions;
using Microsoft.OpenApi.Models;
using WebAPI.Extensions;
using System.Reflection;
using Application.Common.Extensions;
using Infrastructure;
using Infrastructure.Contracts;

var builder = WebApplication.CreateBuilder(args);

var _services = builder.Services;
var _configuration = builder.Configuration;

// Add services to the container.
_services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
_services.AddEndpointsApiExplorer();
_services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.0.0",
        Title = "Seisankanri api",
        Description = "Seisankanri api documents",
        TermsOfService = new Uri("https://example.com/terms")

    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var MyAllowSpecificOrigins = "_myAllowOrigins";

// Register core extention: AutoMapper, FluentValidation
_services.Configure<AuthSetting>(_configuration.GetSection("AuthSetting"));
_services.AddHttpContextAccessor();
_services.AddCoreExtention();
_services.AddInfrastructureServices(_configuration);
_services.AddDatabase(_configuration);
_services.AddServiceContext(_configuration);
_services.AddCoreService(_configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();

