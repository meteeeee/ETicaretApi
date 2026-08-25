using ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.CategoryHandlers;
using ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.ProductHandlers;
using ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.UserRegisterHandlers;
using ETicaretApi.Application.Features.MediatorDesignPattern.Handlers.OrderHandlers;
using ETicaretApi.Persistence.Context;
using ETicaretApi.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DbContext
builder.Services.AddDbContext<ProductContext>();

// Category Handlers
builder.Services.AddScoped<getCategoryQueryHandler>();
builder.Services.AddScoped<getCategoryByIdQueryHandler>();
builder.Services.AddScoped<CreateCategoryCommandHandler>();
builder.Services.AddScoped<UpdateCategoryCommandHandler>();
builder.Services.AddScoped<RemoveCategoryCommandHandler>();

// Product Handlers
builder.Services.AddScoped<getProductQueryHandler>();
builder.Services.AddScoped<getProductByIdQueryHandler>();
builder.Services.AddScoped<CreateProductCommandHandler>();
builder.Services.AddScoped<UpdateProductCommandHandler>();
builder.Services.AddScoped<RemoveProductCommandHandler>();

builder.Services.AddScoped<CreateUserRegisterCommandHandler>();
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<ProductContext>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(getOrderQueryHandler).Assembly));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Api", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Api V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
