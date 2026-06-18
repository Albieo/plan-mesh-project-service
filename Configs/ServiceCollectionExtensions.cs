using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectService.Data;
using ProjectService.Repositories;
using ProjectService.Services;
using ProjectService.Utilities;

namespace ProjectService.Configs;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProjectDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = GetRequiredConfigurationValue(
            configuration.GetConnectionString("DefaultConnection"),
            "Connection string 'DefaultConnection' is not configured.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString, npgsqlOptions =>
                npgsqlOptions.EnableRetryOnFailure()));

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSecret = GetRequiredConfigurationValue(
            configuration["Jwt:Secret"],
            "JWT secret 'Jwt:Secret' is not configured.");

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(JwtSecretDecoder.Decode(jwtSecret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (string.IsNullOrWhiteSpace(context.Token)
                            && context.Request.Cookies.TryGetValue("token", out var token))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }

    public static IServiceCollection AddProjectServiceDependencies(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IFeatureRepository, FeatureRepository>();
        services.AddScoped<IUserStoryRepository, UserStoryRepository>();
        services.AddScoped<ITaskItemRepository, TaskItemRepository>();
        services.AddScoped<IProjectService, ProjectsService>();
        services.AddScoped<IFeatureService, FeatureService>();
        services.AddScoped<IUserStoryService, UserStoryService>();
        services.AddScoped<ITaskItemService, TaskItemService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }

    private static string GetRequiredConfigurationValue(string? value, string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException(errorMessage);
        }

        return value;
    }
}
