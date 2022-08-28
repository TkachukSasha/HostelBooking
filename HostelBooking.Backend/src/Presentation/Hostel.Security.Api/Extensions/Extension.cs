﻿using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Hostel.Security.Api.Extensions
{
    public static class Extension
    {
        public static IServiceCollection AddDefaultServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.WithOrigins(new[] { "http://localhost:3000", "http://localhost:8080", "http://localhost:4200" });
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowCredentials();
                });
            });

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Hostel Security", Version = "v1" });

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey

                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                             {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                             }
                         },
                         new string[] {}
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
