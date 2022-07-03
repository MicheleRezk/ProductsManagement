using Complevo.ProductsManagement;
using Complevo.ProductsManagement.Common;
using Complevo.ProductsManagement.Entities;
using Complevo.ProductsManagement.Services;
using Complevo.ProductsManagement.Settings;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ServiceSettings>(builder.Configuration.GetSection("ServiceSettings"));
builder.Services
    .AddMongo()
    .AddMongoRepository<Product>("Products");
builder.Services.AddSingleton<ISerializerService, SerializerService>();
builder.Services.AddSingleton<IProductServices, ProductServices>();
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
}).AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }