using ApiBook.Application.Interfaces;
using ApiBook.Application.Services;
using ApiBook.Domain.Repositories;
using ApiBook.Infra.Data;
using ApiBook.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

[assembly: System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]

var builder = WebApplication.CreateBuilder(args);

string connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
// Add services to the container.

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BookContext>(options => options.UseSqlServer(connString));

var app = builder.Build();

// Obtém o valor da variável de ambiente X
string awsKeyEncode = Environment.GetEnvironmentVariable("AWS_S3_KEY");

byte[] bytes = Convert.FromBase64String(awsKeyEncode);
string awsKey = System.Text.Encoding.UTF8.GetString(bytes);

var result = awsKey.Split(":");

Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", result[0]);
Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", result[1]);

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BookContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
