using BibliotecaDeClases.DTOs;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApi_DapperService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IDbConnection>(options =>
    new SqlConnection(builder.Configuration.GetConnectionString("RutaSQL"))
);

builder.Services.AddTransient<IBaseService<CazadorDTO_Get, CazadorDTO_PostPut>, CazadorService>();
builder.Services.AddTransient<IBaseService<NenDTO_Get, NenDTO_PostPut>, NenService>();
builder.Services.AddTransient<IBaseService<CazadorNenDTO>, CazadorNenService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
