using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using SmartBank.DAL.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SmartBankDBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("SmartBankDb")));

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
