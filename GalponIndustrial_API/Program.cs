using GalponIndustrial_API;
using GalponIndustrial_API.Context;
using GalponIndustrial_API.Repository;
using GalponIndustrial_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Context
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetConnectionString("Conexion"));

//Repositories
builder.Services.AddScoped<IGalponRepository, GalponRepository>();
builder.Services.AddScoped<INumeroGalponRepository, NumeroGalponRepository>();

//ModelState
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

//AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapping));

//Cors
var myCors = "CorsRules";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: myCors, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(myCors);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
