using DistanceChecker.Configuration;
using DistanceChecker.Interfaces;
using DistanceChecker.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
});
builder.Services.Configure<CityApiConfiguration>(options => builder.Configuration.GetSection("CityApi").Bind(options));
builder.Services.AddTransient<HttpResponseService>();
builder.Services.AddHttpClient<ICityApiService, CityApiService>()
                        .ConfigureHttpClient((serviceProvider, client) =>
                        {
                            Console.WriteLine(serviceProvider.GetRequiredService<IOptions<CityApiConfiguration>>().Value);
                            var cityApi = serviceProvider.GetRequiredService<IOptions<CityApiConfiguration>>().Value;
                            Console.WriteLine(cityApi.ApiUrl); client.BaseAddress = new Uri(cityApi.ApiUrl);

                            client.DefaultRequestHeaders.Add("X-Api-Key", cityApi.ApiKey);
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
