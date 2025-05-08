using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using CoreLayer.DependencyResolvers;
using CoreLayer.Extensions;
using CoreLayer.Utilities.IoC;
using CoreLayer.Utilities.Security.Encryption;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

var cultureInfo = new CultureInfo("tr-TR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    });

builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(options =>
{
    options.RegisterModule(new AutofacBusinessModule());
});

builder.Services.AddDependencyResolvers(new ICoreModule[] {
                new CoreModule()
            });
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<CoreLayer.Utilities.Security.JWT.TokenOptions>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
    };
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

var app = builder.Build();

app.UseCors("AllowOrigin");





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    
    app.UseSwaggerUI();
}
app.ConfigureCustomExceptionMiddleware();
app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
//using Autofac;
//using Autofac.Core;
//using Autofac.Extensions.DependencyInjection;
//using Business.DependencyResolvers.Autofac;
//using CoreLayer.CrossCuttingConcerns.Caching;
//using CoreLayer.DependencyResolvers;
//using CoreLayer.Utilities.IoC;
//using CoreLayer.Utilities.Security.Encryption;
//using CoreLayer.Utilities.Security.JWT;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;

//Console.OutputEncoding = System.Text.Encoding.UTF8;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddCors();
//builder.Services.AddSwaggerGen();
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddSingleton<CoreLayer.CrossCuttingConcerns.Caching.ICacheManager, MemoryCacheManager>();
//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(options => options.RegisterModule(new AutofacBusinessModule())));
//builder.Host.ConfigureContainer<ContainerBuilder>(options =>
//{
//    //builder.Services.AddAuthentication(options =>
//    //{
//    //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//    //}).AddJwtBearer(options =>
//    //{
//    //    options.TokenValidationParameters = new TokenValidationParameters
//    //    {
//    //        ValidateIssuer = true,
//    //        ValidateAudience = true,
//    //        ValidateLifetime = true,
//    //        ValidIssuer = tokenOptions.Issuer,
//    //        ValidAudience = tokenOptions.Audience,
//    //        ValidateIssuerSigningKey = true,
//    //        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
//    //    };
//    //});
//    var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

//    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//        .AddJwtBearer(options =>
//        {
//            options.TokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateIssuer = true,
//                ValidateAudience = true,
//                ValidateLifetime = true,
//                ValidIssuer = tokenOptions.Issuer,
//                ValidAudience = tokenOptions.Audience,
//                ValidateIssuerSigningKey = true,
//                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
//            };
//        });

//    builder.Services.AddDependencyResolvers(new ICoreModule[] {
//                new CoreModule()
//            });
//});

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());
//app.UseStaticFiles();
//app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();
