using Microsoft.EntityFrameworkCore;
using pillpalbackend.Services;
using pillpalbackend.Services.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PillPalService>();
builder.Services.AddScoped<PasswordService>();

var connectionString = builder.Configuration.GetConnectionString("MyConnectionString");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlogPolicy",
    builder =>
    {
        builder.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

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

// app.UseHttpsRedirection();

app.UseCors("BlogPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
