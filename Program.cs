using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using webAPI.DAO;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                      });
});

string  connString = builder.Configuration.GetConnectionString("conexaobd");
builder.Services.AddDbContext<BOSHBENEFICIOContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexaobd"));


});
builder.Services.AddScoped<IRepository,Repository>();
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "WebApi Grupo4 Bosch",
        Description = "Back-End para Api Grupo4 Bosch",        
        Contact = new OpenApiContact
        {
            Name = "Grupo04",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

