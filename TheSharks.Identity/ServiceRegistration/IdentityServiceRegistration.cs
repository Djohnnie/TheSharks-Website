using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Contracts.Services.Members;
using TheSharks.DataAccess.EntityFramework;
using TheSharks.Domain.Entities;
using TheSharks.Identity.Services;

namespace TheSharks.Identity.ServiceRegistration;

public static class IdentityServiceRegistration
{
    public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IMemberService<Member>, MemberService>();
        services.AddScoped<IRoleService<Role>, RoleService>();
        services.AddIdentity<Member, Role>()
            .AddEntityFrameworkStores<TheSharksContext>()
            .AddDefaultTokenProviders();
        services.AddDataProtection().PersistKeysToDbContext<TheSharksContext>();
        services.Configure<IdentityOptions>(o =>
        {
            o.Tokens.ProviderMap.Add("CustomPasswordReset",
                new TokenProviderDescriptor(typeof(CustomPasswordResetTokenProvider<Member>)));
            o.Password.RequiredLength = 8;
            o.Password.RequireDigit = true;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequireUppercase = false;
            o.Tokens.PasswordResetTokenProvider = "CustomPasswordReset";
        });
        services.Configure<DataProtectionTokenProviderOptions>(o =>
            o.TokenLifespan = TimeSpan.FromDays(7));
        services.AddTransient<CustomPasswordResetTokenProvider<Member>>();
        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = bool.Parse(configuration["JWT:RequireHttps"]);
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("ManageMembersPolicy", policy => policy.RequireClaim("ManageMembers"));
            options.AddPolicy("ManageActivitiesPolicy", policy => policy.RequireClaim("ManageActivities"));
            options.AddPolicy("ManageNewsItemsPolicy", policy => policy.RequireClaim("ManageNewsItems"));
            options.AddPolicy("ManageDownloadablesPolicy", policy => policy.RequireClaim("ManageDownloadables"));
            options.AddPolicy("ManageGalleriesPolicy", policy => policy.RequireClaim("ManageGalleries"));
            options.AddPolicy("ManagePageContentPolicy", policy => policy.RequireClaim("ManagePageContent"));
            options.AddPolicy("ManageStatisticsPolicy", policy => policy.RequireClaim("ManageStatistics"));
        });
    }
}