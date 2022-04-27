using AuthentificationWithAccounts.Configurations;
using AuthentificationWithAccounts.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyUserIdentityDb"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

//Lese aus appsettings.json den Konfigurationspunkt -> JwtBearerTokenSettings
IConfigurationSection jwtSection = builder.Configuration.GetSection("JwtBearerTokenSettings");

//Übertrage die Konfiguration von JwtBearerTokenSettings in die Klasse JwtBearerTokenSettings
builder.Services.Configure<JwtBearerTokenSettings>(jwtSection); //DI Container verfügbar

//Die Klassen JwtBearerTokenSettings (mit Konfigurationen) werden wir auch gleich verwenden 
JwtBearerTokenSettings jwtBearerTokenSettings = jwtSection.Get<JwtBearerTokenSettings>();

byte[] key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = jwtBearerTokenSettings.Issuer,  //"Issuer": "https://localhost:44322/"
        ValidateAudience = true,
        ValidAudience = jwtBearerTokenSettings.Audience, //"Audience": "https://localhost:44322/",
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Middleware -> muss 
app.UseCors(x => x
       .AllowAnyOrigin()
       .AllowAnyMethod()
       .AllowAnyHeader());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); //steht immer vor Authorization (muss)
app.UseAuthorization();

app.MapControllers();

app.Run();
