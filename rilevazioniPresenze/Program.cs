using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using rilevazioniPresenza.Reps.UserFiles;
using rilevazioniPresenzaData;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TEST API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }
            });
});

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
//    .AddCookie(options =>
//    {
//        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
//        options.Cookie.Name = "YourAuthCookie"; // Nome del cookie
//        options.Cookie.HttpOnly = true; // Rende il cookie accessibile solo via HTTP (sicurezza)
//        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Durata del cookie
//        options.SlidingExpiration = true; // Estende la durata ad ogni richiesta valida
//        options.LoginPath = null; // Percorso di reindirizzamento per utenti non autenticati
//        options.LogoutPath = null; // Percorso di reindirizzamento per il logout
//        options.AccessDeniedPath = null; // Percorso per accesso negato
//        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Assicura che il cookie sia inviato solo su HTTPS
//        options.Cookie.SameSite = SameSiteMode.None; // O Strict, None a seconda delle tue esigenze
//        options.Events = new CookieAuthenticationEvents
//        {
//            OnRedirectToLogin = context =>
//            {
//                context.Response.StatusCode = 401;
//                Console.WriteLine($"DEBUG: RedirectToLogin event triggered. Path: {context.RedirectUri}");
//                // Non chiamare context.Response.Redirect() qui
//                // Puoi provare a chiamare context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
//                // e context.HandleResponse(); per sopprimere il reindirizzamento predefinito
//                return Task.CompletedTask;
//            },
//            OnRedirectToAccessDenied = context =>
//            {
//                Console.WriteLine($"DEBUG: RedirectToAccessDenied event triggered. Path: {context.RedirectUri}");
//                // context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
//                // context.HandleResponse();
//                return Task.CompletedTask;
//            },
//            OnRedirectToLogout = context =>
//            {
//                Console.WriteLine($"DEBUG: RedirectToLogout event triggered. Path: {context.RedirectUri}");
//                return Task.CompletedTask;
//            }
//        };
//    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("questaèunachiavesegretapiùsicura123"))
        };
    });

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string"
        + "'DefaultConnection' not found.");

builder.Services.AddDbContext<RilevazionePresenzaContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("allowall", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
});

builder.Logging.AddConsole();
builder.Logging.AddDebug();
//builder.Logging.AddFile("logs/myapp-{Date}.txt", LogLevel.Information);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("allowall");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
