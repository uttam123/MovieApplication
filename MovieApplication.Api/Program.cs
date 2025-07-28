using MovieApplication.Api.Repository;
using MovieApplication.Api.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

// Swagger setup (only once)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Movie Library API",
        Version = "v1",
        Description = "API for managing movies from the Blazor frontend",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Uttam Kumar Santra",
            Email = "uttam.bbsr@gmail.com"
        }
    });
});

var app = builder.Build();

// Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Library API");
    c.RoutePrefix = string.Empty; 
    // Serve Swagger at root (https://localhost:7125/)
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
