using AddisEats.Api;
using Microsoft.AspNetCore.Authentication.Cookies;
using AddisEats.Api.Services.RestaurantServices;
using System.Text.Json.Serialization;
using AddisEats.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

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
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
});
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
app.UseRouting();
app.Use(async (context, next) =>
{
    if (context.Request.Query.Count > 0)
    {
        var action = context.GetEndpoint()?.Metadata.GetMetadata<ControllerActionDescriptor>();
        var isRestaurantList = string.Equals(
            action?.AttributeRouteInfo?.Template,
            "api/v1/restaurants",
            StringComparison.OrdinalIgnoreCase);
        var allowed = isRestaurantList
            ? new HashSet<string>(["name", "area", "areas", "maxFeeEtb", "page", "limit"], StringComparer.OrdinalIgnoreCase)
            : new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var unknown = context.Request.Query.Keys.Where(key => !allowed.Contains(key)).ToArray();
        if (unknown.Length > 0)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/problem+json";
            var problem = new ProblemDetails
            {
                Type = "https://addiseats.api/problems/unknown-query-parameter",
                Title = "The request has an unknown query parameter.",
                Status = StatusCodes.Status400BadRequest,
                Detail = $"Remove or rename these query parameters: {string.Join(", ", unknown)}.",
                Instance = context.Request.Path
            };
            problem.Extensions["code"] = "unknown_query_parameter";
            await context.Response.WriteAsJsonAsync(problem);
            return;
        }
    }

    await next();
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
