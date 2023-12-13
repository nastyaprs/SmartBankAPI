using Microsoft.EntityFrameworkCore;
using SmartBank.BLL.Interfaces.IRepositories;
using SmartBank.BLL.Interfaces.IServices;
using SmartBank.BLL.Repositories;
using SmartBank.BLL.Services;
using SmartBank.DAL.Data;
using SmartBank.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SmartBankDBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("SmartBankDb")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
