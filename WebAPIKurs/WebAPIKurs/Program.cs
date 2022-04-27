using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebApiContrib.Core.Formatter.Csv;
using WebAPIKurs.Data;
using WebAPIKurs.Services;
//WebApplication.CreateBuiler Factory-Pattern

//WebApplicationBuilder kümmert sich um die Initiasierung der WebAPP
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ToDoDbContext>(options =>
//WebApplication.CreateBuiler Factory-Pattern

//WebApplicationBuilder kümmert sich um die Initiasierung der WebAPP
    options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoDbContext") ?? throw new InvalidOperationException("Connection string 'ToDoDbContext' not found.")));

builder.Services.AddDbContext<MovieDbContext>(); //In Controller benötige ich 

builder.Services.AddHttpClient<IVideoService, VideoService>();
#region WebApplicationBuilder ist abwärtskomatibel
//builder.Host->IHostBuilder ASP.NET Core 3.1 + 5.0 

//builder.WebHost -> IWebHostBuilder -> IWebhostBuilder
#endregion


//WebApplication.CreateBuiler Factory-Pattern

//WebApplicationBuilder k�mmert sich um die Initiasierung der WebAPP


// Add services to the container.
//builder.Services -> ServiceCollection zu initialisieren unserer Dienste 



//AddController besagt, dass wir eine WebAPI verwenden 
builder.Services.AddControllers()
    .AddXmlSerializerFormatters()
    .AddCsvSerializerFormatters();

//IConfiguration wird automatisch bef�llt (alle Konfoigurationsdaten werden in IConfiguration aufgelistet) 

builder.Services.AddSingleton<ICar, MockCar>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//SwaggerGen mit der Möglichkeit Swagger-Kommentare an Get/Post/Put/Delete Methoden zu hinterlegen
builder.Services.AddSwaggerGen( options =>
{
    string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

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
