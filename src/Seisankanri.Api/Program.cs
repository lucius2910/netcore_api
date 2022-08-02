using Business.Core.Extensions;
using Business.Inventories.Extensions;
using Business.Logs.Extensions;
using Business.Plan.Extensions;
using Business.SaleOrder.Extensions;
using Framework.Api.Extensions;
using Microsoft.OpenApi.Models;
using Seisankanri.Api.Extensions;
using Seisankanri.Model;
using System.Reflection;

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
_services.AddHttpContextAccessor();
_services.AddCoreExtention();
_services.AddDatabase(_configuration);
_services.AddServiceContext(_configuration);
_services.AddCORS(MyAllowSpecificOrigins);
_services.AddCoreService(_configuration);
_services.AddPlanService(_configuration);
_services.AddSaleOrderService(_configuration);
_services.AddInventoryService(_configuration);
_services.AddLogsService(_configuration);

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.ConfigureCoreDb();

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();

