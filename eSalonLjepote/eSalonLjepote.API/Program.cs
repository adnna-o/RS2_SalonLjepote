using eSaljonLjepote.Services.Service;
using eSalonLjepote.API;
using eSalonLjepote.API.Filters;
using eSalonLjepote.Service.Database;
using eSalonLjepote.Service.RabbitMQ;
using eSalonLjepote.Service.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ========================================
//  DEPENDENCY INJECTION
// ========================================
builder.Services.AddTransient<IKorisnikService, KorisnikService>();
builder.Services.AddTransient<IAdministratorService, AdministratorService>();
builder.Services.AddTransient<IUslugaService, UslugaService>();
builder.Services.AddTransient<IZaposleniService, ZaposleniService>();
builder.Services.AddTransient<IUlogaService, UlogaService>();
builder.Services.AddTransient<IKlijentiService, KlijentiService>();
builder.Services.AddTransient<ITerminiService, TerminiService>();
builder.Services.AddTransient<IRadnoVrijemeService, RadnoVrijemeService>();
builder.Services.AddTransient<ISalonLjepoteService, SalonLjepoteService>();
builder.Services.AddTransient<IProizvodService, ProizvodiService>();
builder.Services.AddTransient<IRecenzijeService, RecenzijeService>();
builder.Services.AddTransient<IPlacanjeService, PlacanjeService>();
builder.Services.AddTransient<IGalerijaService, GalerijaService>();
builder.Services.AddTransient<INovostiService, NovostiService>();
builder.Services.AddTransient<IKorisnikUlogaService, KorisnikUlogaService>();
builder.Services.AddTransient<INarudzbaService, NarudzbaService>();
builder.Services.AddTransient<IKorpaService, KorpaService>();
builder.Services.AddTransient<IOcjeneProizvodaService, OcjeneProizvodaService>();
builder.Services.AddTransient<INarudzbaStavka, NarudzbaStavkaService>();

builder.Services.AddScoped<IMailProducer, MailProducer>();

// ========================================
//  CONTROLLERS + JSON CONFIG
// ========================================
builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add<ErrorFilter>();  // globalni error handling
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };
        options.SerializerSettings.ReferenceLoopHandling =
            Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// ========================================
//  SWAGGER / OPENAPI
// ========================================
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.FullName);

    c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        Description = "Unesite username i password"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Basic"
                }
            },
            new string[] {}
        }
    });
});

// ========================================
//  DATABASE
// ========================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ESalonLjepoteContext>(options =>
    options.UseSqlServer(connectionString));

// ========================================
//  AUTOMAPPER
// ========================================
builder.Services.AddAutoMapper(typeof(IKorisnikService));

// ========================================
//  AUTHENTICATION
// ========================================
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(
        "BasicAuthentication", null);

// ========================================
//  BUILD APP
// ========================================
var app = builder.Build();

// ========================================
//  MIDDLEWARE PIPELINE
// ========================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ========================================
//  AUTOMATSKE MIGRACIJE
// ========================================
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ESalonLjepoteContext>();
    try
    {
        dataContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine("DB migration failed: " + ex.Message);
    }
}

app.Run();
