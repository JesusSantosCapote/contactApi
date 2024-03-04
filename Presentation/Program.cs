using BusinessLogic.DTO;
using BusinessLogic.Services;
using DataAccess.DataContexts;
using DataAccess.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Presentation.Authentication;
using Presentation.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add custom services
builder
    .AddSwaggerGen()
    .AddJwtBearerAuthentication()
    .AddAuthorization();

builder.Services.AddDbContext<UserManagerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    //options.UseInMemoryDatabase("DefaultConnection");
});

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IValidator<ContactDto>, ContactDtoValidator>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IUserContext, UserContext>();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();



app.Run();
