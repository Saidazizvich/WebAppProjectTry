using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Presentation.ActionFilter;
using Repositories.EfCore;
using Services;
using Services.Concreate;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container. // IoC yeridir burdan asaya kadar uygulamayi kaydet ediyoruz --builder ile

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;  // simdi burda */* content Content Negotiation acik yaptik ve  data gostere biliriz
    config.ReturnHttpNotAcceptable = true; // biz burda ne yaptik burda biz diseriye kendi data gosteryoruz yani text cevirib kullanici gosteryoruz
})
    .AddCustomerCsvFormater()
    .AddNewtonsoftJson().AddXmlDataContractSerializerFormatters().AddApplicationPart(typeof(Presentation.AssemblyRefence).Assembly);

builder.Services.AddScoped<ValidationFilterAttribute>(); //ioc 
 

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager(); // ioc eklendi
builder.Services.ConfigureServiceManager(); 
builder.Services.ConfigureLoggerService(); // burda esa loglama islemi eklendi 
builder.Services.AddAutoMapper(typeof(Program));// burda esa mapping islemini kayd yaptik
builder.Services.ConfigureActionResult();  
builder.Services.ConfigureDataShaping();
builder.Services.AddCustomMediaTypes();
builder.Services.AddScoped<IBookLinks, BookLinks>();


var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExeptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if(app.Environment.IsProduction())
{
    app.UseHsts();   
}     

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
 
app.Run();
