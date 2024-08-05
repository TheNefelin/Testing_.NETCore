using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi_Auth.Connections;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Servicio Conexion -------------------------------------------------
builder.Services.AddDbContext<WebApiDbContext>(opcion =>
    opcion.UseSqlServer(builder.Configuration.GetConnectionString("RutaSQL"))
);
// -------------------------------------------------------------------

// Agrega Identity a los EndPonts para autenticar --------------------
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<WebApiDbContext>();
// -------------------------------------------------------------------

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

// Agrega los EndPoint de Identity -----------------------------------
app.MapIdentityApi<IdentityUser>();
// -------------------------------------------------------------------

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
