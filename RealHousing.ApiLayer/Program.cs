using RealHousing.ApiLayer.Controllers;
using RealHousing.BusinessLayer.Abstract;
using RealHousing.BusinessLayer.Concreate;
using RealHousing.DataAccessLayer.Abstract;
using RealHousing.DataAccessLayer.Concreate;
using RealHousing.DataAccessLayer.EntityFreamework;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Context>();

builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddScoped<IProductDal, EfProductDal>();
builder.Services.AddScoped<IProductService, ProductManager>();

//builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();

//builder.Services.AddHttpClient<CategoryController>(client =>
//{
//    client.BaseAddress = new Uri("https://localhost:44352/api/Category");
//    client.DefaultRequestHeaders.Add("Accept", "application/json");
//});
//builder.Services.AddHttpClient<ICategoryService>();

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
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
