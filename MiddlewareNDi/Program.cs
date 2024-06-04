using Microsoft.Extensions.DependencyInjection.Extensions;
using MiddlewareNDi.Dependencies;
using MiddlewareNDi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IOperationTransient, Operation>();
builder.Services.AddScoped<IOperationScoped, Operation>();
builder.Services.AddSingleton<IOperationSingleton, Operation>();
builder.Services
           .TryAddEnumerable(ServiceDescriptor.Singleton<IOperationSingletonInstance, Operation>(a => new Operation(Guid.Empty)));

builder.Services.AddTransient<DependencyService1, DependencyService1>();
builder.Services.AddTransient<DependencyService2, DependencyService2>();

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

app.UseMiddleware<Middleware>();
app.UseMiddleware<Middleware1>();

app.Run();
