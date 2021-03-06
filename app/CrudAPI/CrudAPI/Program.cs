using CrudAPI.Models;
using CrudAPI.Services;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.Configure<CustomerDatabaseSettings>
    (builder.Configuration.GetSection("DevNetStoreDatabase"));

builder.Services.AddSingleton<CustomerServices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c =>
    {
        c.AllowAnyOrigin();
        c.AllowAnyMethod();
        c.AllowAnyHeader();
    });

app.UseAuthorization();

app.MapControllers();

app.Run();
