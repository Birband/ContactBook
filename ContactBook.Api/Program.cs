using ContactBook.Application.Services.User;
using ContactBook.Infrastructure.DI;
using ContactBook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddDbContext<ContactBookDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddSwaggerGen();
    }
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();        
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.MapControllers();
    app.Run();
}
