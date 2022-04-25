using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPIKurs.Data;
//WebApplication.CreateBuiler Factory-Pattern

//WebApplicationBuilder k�mmert sich um die Initiasierung der WebAPP
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MovieDbContext>(); //In Controller benötige ich 
//WebApplication.CreateBuiler Factory-Pattern

//WebApplicationBuilder k�mmert sich um die Initiasierung der WebAPP


// Add services to the container.
//builder.Services -> ServiceCollection zu initialisieren unserer Dienste 


builder.Services.AddControllers(); //AddController besagt, dass wir eine WebAPI verwenden 
//IConfiguration wird automatisch bef�llt (alle Konfoigurationsdaten werden in IConfiguration aufgelistet) 

builder.Services.AddSingleton<ICar, MockCar>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//WebApplicationBuilder abw�rtskompatibel (obselete in .NET 6.0) 
//builder.Host  -> IHostBuilder -> ASP.NET Core 3.1 / 5.0 
//builder.WebHost -> IWebHostBuilder -> ASP.NET Core 2.x

WebApplication app = builder.Build(); //Beenden der Inializierungs-Phase mithilfe des WebApplicationBuilders

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); //MapControllers regelt die Request zu unseren WebAPI - Controller  

app.Run();
