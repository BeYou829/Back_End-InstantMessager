using server.Hubs;
using Chat.Infrastructure.Persistence;
using Chat.Infrastructure.Jwt;
using Chat.Core.Application;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSignalR();


builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddJWTAuthorization(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
        policy =>
        {
            policy
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins("http://localhost:5173", "http://localhost:5270");
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors("_myAllowSpecificOrigins");

app.MapHub<ChatHub>("/chat").RequireAuthorization();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
