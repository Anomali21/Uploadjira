using NRE_Portal.DAL.Interfaces;
using NRE_Portal.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// register DAL repository (production default: use instance without path)
builder.Services.AddScoped<IEnergyRepository, EnergyRepository>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

// Ajouter Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();

public partial class Program { }
