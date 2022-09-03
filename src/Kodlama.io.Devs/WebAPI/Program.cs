
using Core.CrossCuttingConcerns.Exceptions;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddPersistenceServices(builder.Configuration);
//builder.Services.AddApplicationService();

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


//if (app.Environment.IsProduction())
//app.ConfigureCustomExceptionMiddleware();


app.UseAuthorization();

app.MapControllers();

app.Run();
