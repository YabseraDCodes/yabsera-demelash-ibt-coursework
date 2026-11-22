using AddisEats.Api;
using Microsoft.AspNetCore.Authentication.Cookies;
using AddisEats.Api.Services.RestaurantServices;
using System.Text.Json.Serialization;
using AddisEats.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddAuthorization();
builder.Services.AddSingleton<IClock, CapturedClock>();
builder.Services.AddScoped<IPaymentProvider, CbeBirrProvider>();
builder.Services.Configure<DeliveryOptions>(builder.Configuration.GetSection("Delivery"));
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddSingleton<IOrderService, OrderService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
