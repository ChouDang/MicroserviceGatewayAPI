using MongoDB.Driver;
using Product.Microservice.Models;
using Product.Microservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMongoClient, MongoClient>();
builder.Services.Configure<ProductDatabaseSettings>(
    builder.Configuration.GetSection("Database"));
builder.Services.AddSingleton<ProductsServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EFCore.CodeFirst.WebApi");
});

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
