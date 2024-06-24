using ContactBook.Api.Middleware;
using ContactBook.Application.Services.User;
using ContactBook.Domain.Entities;
using ContactBook.Infrastructure.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddInfrastructure(builder.Configuration);

    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new() { Title = "ContactBook.Api", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""    
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });
        });


    }
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();        
        app.ApplyMigration();
    }

    app.UseMiddleware<ErrorHandler>();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
