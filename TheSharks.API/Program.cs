using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TheSharks.API.ExceptionMiddleware;
using TheSharks.Application.ServiceRegistration;
using TheSharks.DataAccess.ServiceRegistration;
using TheSharks.Identity.ServiceRegistration;
using TheSharks.Mail.ServiceRegistration;
using TheSharks.Storage.ServiceRegistration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddMailServices(builder.Configuration);
builder.Services.AddStorageServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.AddSpaStaticFiles(o => o.RootPath = "wwwroot");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://thesharks.be", "https://www.thesharks.be", "https://test.thesharks.be")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseSpaStaticFiles();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddlewareHandler>();

app.UseSpa(spa => spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
{
    OnPrepareResponse = context =>
    {
        context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
        context.Context.Response.Headers.Add("Expires", "-1");
    }
});

app.Run();