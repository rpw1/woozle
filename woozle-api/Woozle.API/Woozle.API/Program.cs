using Woozle.API.Features;
using Woozle.API.Spotify;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
	{
		builder.AllowAnyOrigin()
			.AllowAnyHeader()
			.AllowAnyMethod();
	});
});

builder.Services.AddOpenApi();

builder.Services.RegisterSpotifyServices(builder.Configuration);
builder.Services.RegisterFeatureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
	app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Health check was successful!");

app.Run();
