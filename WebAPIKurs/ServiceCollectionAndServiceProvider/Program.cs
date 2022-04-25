using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Test");


#region Einstieg - Inversion of Control (Depencendy Inversion)
//Test-Objekt
ICar mockCar = new MockCar();

ICar car1 = new Car();

ICarService carService = new CarService();
carService.Repair(mockCar);
carService.Repair(car1);
#endregion



#region IOC Container 

//Im der ServiceCollection werden beim Programmstart (Inital) die Services hinzugefügt -> AddDbContext (EF Core Anbindung) / 

IServiceCollection serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<ICar, MockCar>(); // Es gibt nur eine Instanziierung

#region Erweitertes Beispiel
//Achtung hier wird überschrieben -> ist doof -> besser 
//Wenn ein Interface in mehrere implementierten Klassen vorkommen und wir diese Klassen (Dienste) verwenden wollen, benötigen wir Interface-Vererbung
//serviceCollection.AddScoped<IScopedCar, Car>();
#endregion
// serviceCollection.BuildServiceProvider(); Schließt die Initalisierung Phase ab 


IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

//MockCar
//GetService vs. GetRequiredService 

//Wenn nicht gefunden, wird NULL zurück gegeben
ICar car = serviceProvider.GetService<ICar>();

//Wenn nicht gefunden,dann Exception 
ICar car2 = serviceProvider.GetRequiredService<ICar>(); //Controller-Klasse -> Konstrutkor Injection / Methoden - Injection [FromServices] 



#endregion




#region BadCode -> Feste Kopplung 

//Problem darin ist, dass bei Änderungen in BadCar alles in BadCarService.Repair getestet werden
public class BadCar
{
    string Marke { get; set; }
    string Modell { get; set; } 
}

public class BadCarService
{
    void Repair(BadCar car)
    {

    }
}
#endregion



#region Inversion of Control

//Contract First -> Interfaces anhand einer Spefikation erstellen 
public interface ICar
{
    string Marke { get; set; }
    string Modell { get; set; } 
}

public interface IScopedCar : ICar
{

}

public interface ICarService
{
    void Repair(ICar car);
}


//Programmierer können parallel an Klassen arbeiten

//Programmierer A: 5 Tage
public class Car : ICar, IScopedCar
{
    public string Marke { get; set; }
    public string Modell { get; set; }
}

//Programmier B: 3 Tage 
public class CarService : ICarService
{
    public void Repair (ICar car)
    {

    }
}

public class MockCar : ICar
{
    public string Marke { get; set; } = "VW";
    public string Modell { get; set; } = "Polo";
}

#endregion