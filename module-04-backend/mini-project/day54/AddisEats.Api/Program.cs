using AddisEats.Api.Services.MenuServices;
using AddisEats.Api.Services.RestaurantServices;
using AddisEats.Api.Services;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var details = new ProblemDetails
        {
            Type = "https://addiseats.api/problems/invalid-request",
            Title = "The request is not valid.",
            Status = StatusCodes.Status400BadRequest,
            Detail = "Check the request values and try again.",
            Instance = context.HttpContext.Request.Path
        };
        details.Extensions["code"] = "invalid_request";
        details.Extensions["errors"] = context.ModelState
            .Where(entry => entry.Value?.Errors.Count > 0)
            .ToDictionary(entry => entry.Key, entry => entry.Value!.Errors.Select(error => error.ErrorMessage));
        return new BadRequestObjectResult(details);
    };
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy("Frontend", policy =>
    policy.WithOrigins(builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [])
        .AllowAnyHeader()
        .AllowAnyMethod()));
builder.Services.Configure<MenuOptions>(
    builder.Configuration.GetSection(MenuOptions.SectionName));
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddSingleton<IOrderService, OrderService>();

var app = builder.Build();

app.UseExceptionHandler(errorApp => errorApp.Run(async context =>
{
    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    context.Response.ContentType = "application/problem+json";
    var problem = new ProblemDetails
    {
        Type = "https://addiseats.api/problems/server-error",
        Title = "Something went wrong.",
        Status = StatusCodes.Status500InternalServerError,
        Detail = "The API could not complete the request. Try again later.",
        Instance = context.Request.Path
    };
    problem.Extensions["code"] = "server_error";
    await context.Response.WriteAsJsonAsync(problem);
}));
app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;
    var code = response.StatusCode;
    response.ContentType = "application/problem+json";
    var problem = new ProblemDetails
    {
        Type = $"https://addiseats.api/problems/{code}",
        Title = code == 404 ? "The requested item was not found." : "The request could not be completed.",
        Status = code,
        Detail = code == 404 ? "Check the path and any IDs, then try again." : "Check the request and try again.",
        Instance = context.HttpContext.Request.Path
    };
    problem.Extensions["code"] = code == 404 ? "not_found" : "http_error";
    await response.WriteAsJsonAsync(problem);
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("Frontend");
app.UseAuthorization();
app.MapControllers();

app.Run();
