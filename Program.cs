using Validation_Project.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddHttpClient(Constants.RestCountries.HttpClientName, client =>
{
    client.BaseAddress = new Uri(Constants.RestCountries.BaseAddress);
});

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
});

app.Run();
