using AlertaSolo.Data;
using AlertaSolo.Services;
using AlertaSolo.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conexão com banco (exemplo com Oracle — altere se necessário)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Injeção de dependência dos serviços
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ILocalRiscoService, LocalRiscoService>();
builder.Services.AddScoped<ISensorService, SensorService>();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(p =>
    {
        p.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
