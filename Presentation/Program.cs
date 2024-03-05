using BusinessLogic.DTO;
using BusinessLogic.Services;
using DataAccess;
using DataAccess.DataContexts;
using DataAccess.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging();
    //options.UseInMemoryDatabase("DefaultConnection");
});

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IValidator<ContactDto>, ContactDtoValidator>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IUserContext, UserContext>();

var app = builder.Build();

//Seed Database
using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    var context = scopedProvider.GetRequiredService<UserManagerContext>();
    context.Database.EnsureCreated();
    await DataSeed.Seed(context);
}

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
