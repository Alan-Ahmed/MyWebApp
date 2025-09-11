using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

using MyWebApp.ApplicationLayer.Common.Behaviors;    // ValidationBehavior, LoggingBehavior
using MyWebApp.ApplicationLayer.Features.Products.Handlers;
using MyWebApp.ApplicationLayer.Interfaces;          // IRepository<>, IProductRepository
using MyWebApp.ApplicationLayer.Mappings;            // MappingProfile
using MyWebApp.InfrastructureLayer.Persistence;      // AppDbContext
using MyWebApp.InfrastructureLayer.Repositories;     // Repository<>, ProductRepository

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// DbContext (InMemory för dev/test)
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("TestDb"));

// MediatR 13 – registrering via marker type
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateProductHandler).Assembly);
});

// ✅ AutoMapper – config-action (INGA extra argument)
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

// FluentValidation + MediatR pipeline
builder.Services.AddValidatorsFromAssembly(typeof(MappingProfile).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>)); // valfritt

// Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
