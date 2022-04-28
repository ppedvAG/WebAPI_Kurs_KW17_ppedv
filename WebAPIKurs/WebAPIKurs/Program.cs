using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System.Reflection;
using WebApiContrib.Core.Formatter.Csv;
using WebAPIKurs.Data;
using WebAPIKurs.Services;



#region WebApplicationBuilder builder = WebApplication.CreateBuilder(args); Comments
//WebApplication.CreateBuiler Factory-Pattern

//WebApplicationBuilder kümmert sich um die Initiasierung der WebAPP


//WebApplication.CreateBuiler Factory-Pattern

//WebApplicationBuilder k�mmert sich um die Initiasierung der WebAPP


// Add services to the container.
//builder.Services -> ServiceCollection zu initialisieren unserer Dienste 
#endregion
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);



#region EF Core Einbindung (MSSQL + LocalDb Provider -> UseSqlServer UND InMemoryDatabase - Provider)


builder.Services.AddDbContext<ToDoDbContext>(options =>
{
    //Einbindung des SQL-Providers
    options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoDbContext") ?? throw new InvalidOperationException("Connection string 'ToDoDbContext' not found."));


    //MemoryDatabase
    //options.UseInMemoryDatabase("MyMemoryDatabaseForEvaluations"); //Leere DB bei WebApp-Start 
});


builder.Services.AddDbContext<MovieDbContext>(); //In Controller benötige ich 
#endregion

//Broadcast-Video Beispiel
builder.Services.AddHttpClient<IVideoService, VideoService>();

//File-Upload / Download Service
builder.Services.AddTransient<IFileService, FileService>();


#region Serilog-Implementierung

//Configuration per Code
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("Log\\log-.txt", rollingInterval: RollingInterval.Day) //jeden Tag eine neu Logdatei 
            .CreateLogger();

builder.Host.UseSerilog();
#endregion


#region WebApplicationBuilder ist abwärtskomatibel
//builder.Host->IHostBuilder ASP.NET Core 3.1 + 5.0 

//builder.WebHost -> IWebHostBuilder -> IWebhostBuilder
#endregion






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

#region Beispiel EF-Core (InMemoryDB - für Testdaten einbinden)
IServiceScope scope = app.Services.CreateScope();
ToDoDbContext toDoDbContext = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
DataSeeder.SeedTodoDb(toDoDbContext);
#endregion



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); //MapControllers regelt die Request zu unseren WebAPI - Controller  


try
{
    Log.Information("Starting web host");
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
