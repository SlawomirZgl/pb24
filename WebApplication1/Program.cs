using BLL.ServiceInterfaces;
using BLL_DB;
using BLL_EF;
using DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBasketService, BasketService>();
//builder.Services.AddScoped<IOrderService, >();
builder.Services.AddScoped<IProductsService, ProdutsService>();
builder.Services.AddScoped<IWebShopRepo, WebShopRepo>();
builder.Services.AddDbContext<WebShopContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
