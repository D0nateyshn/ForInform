using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

public enum FlightStatus
{
    OnTime,
    Delayed,
    Cancelled,
    Boarding,
    InFlight
}

public class Flight
{
    public string FlightNumber { get; set; }
    public string Airline { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public FlightStatus Status { get; set; }
    public TimeSpan Duration { get; set; }
    public string AircraftType { get; set; }
    public string Terminal { get; set; }
}

public class FlightInformationSystem
{
    private readonly List<Flight> flights = new List<Flight>();

    public void LoadDataFromJsonFile(string filePath)
    {
        try
        {
            string jsonData = File.ReadAllText(filePath);
            var flightsData = JsonConvert.DeserializeObject<FlightData>(jsonData);

            ValidateAndProcessData(flightsData);
            flights.AddRange(flightsData.Flights);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data from JSON file: {ex.Message}");
         
        }
    }

    private void ValidateAndProcessData(FlightData flightsData)
    {
       
    }

    public void DisplayFlightInformation()
    {
        foreach (var flight in flights)
        {
            Console.WriteLine($"Flight {flight.FlightNumber} - {flight.Status}");
        }
    }

    public List<Flight> GetFlightsByAirline(string airline)
    {
        return flights
            .Where(f => f.Airline == airline)
            .OrderBy(f => f.DepartureTime)
            .ToList();
    }

    public List<Flight> GetDelayedFlights()
    {
        return flights
            .Where(f => f.Status == FlightStatus.Delayed)
            .OrderBy(f => f.ArrivalTime)
            .ToList();
    }

    public List<Flight> GetFlightsByDepartureDate(DateTime departureDate)
    {
        return flights
            .Where(f => f.DepartureTime.Date == departureDate.Date)
            .OrderBy(f => f.DepartureTime)
            .ToList();
    }

    public List<Flight> GetFlightsInTimeRangeAndDestination(DateTime startTime, DateTime endTime, string destination)
    {
        return flights
            .Where(f => f.DepartureTime >= startTime && f.DepartureTime <= endTime && f.Destination == destination)
            .OrderBy(f => f.DepartureTime)
            .ToList();
    }

    public List<Flight> GetFlightsArrivedLastHourOrInTimeRange(DateTime endTime)
    {
        var startTimeLastHour = endTime.AddHours(-1);
        return flights
            .Where(f => (f.ArrivalTime >= startTimeLastHour && f.ArrivalTime <= endTime) || f.ArrivalTime <= endTime)
            .OrderBy(f => f.ArrivalTime)
            .ToList();
    }
}

public class FlightData
{
    public List<Flight> Flights { get; set; }
}

class Program
{
    static void Main()
    {
        FlightInformationSystem flightSystem = new FlightInformationSystem();
        flightSystem.LoadDataFromJsonFile("flights.json");

        var flightsByAirline = flightSystem.GetFlightsByAirline("WizAir");
        var delayedFlights = flightSystem.GetDelayedFlights();
        var flightsOnDate = flightSystem.GetFlightsByDepartureDate(DateTime.Parse("2023-08-23"));
        var flightsInTimeRangeAndDestination = flightSystem.GetFlightsInTimeRangeAndDestination(
            DateTime.Parse("2023-05-1T00:00:01"), DateTime.Parse("2023-05-31T23:59:59"), "Kharkiv");
        var flightsArrivedLastHourOrInTimeRange = flightSystem.GetFlightsArrivedLastHourOrInTimeRange(DateTime.Now);

        Console.WriteLine("All flights:");
        flightSystem.DisplayFlightInformation();

        Console.WriteLine("\nFlights by WizAir:");
        DisplayFlightsInfo(flightsByAirline);

        Console.WriteLine("\nDelayed flights:");
        DisplayFlightsInfo(delayedFlights);

        Console.WriteLine("\nFlights on 2023-08-23:");
        DisplayFlightsInfo(flightsOnDate);

        Console.WriteLine("\nFlights in May to Kharkiv:");
        DisplayFlightsInfo(flightsInTimeRangeAndDestination);

        Console.WriteLine("\nFlights arrived last hour or in time range:");
        DisplayFlightsInfo(flightsArrivedLastHourOrInTimeRange);
    }

    private static void DisplayFlightsInfo(List<Flight> flights)
    {
        foreach (var flight in flights)
        {
            Console.WriteLine($"Flight {flight.FlightNumber} - {flight.Status}");
        }
    }
}


