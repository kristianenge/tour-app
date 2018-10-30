using System;
using System.Linq;
using System.Collections.Generic;
using tourApp.FileUtil;
using tourApp.Model;

namespace tourApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var jsonFile = FileReader.ReadEmbeddedFile("tourApp.data.cities.json");
            var cities = City.FromJson(jsonFile);
            
            var AllTours = new List<String>();
            foreach(var c in cities){
                AllTours.Add(doRoute(cities,c.Name));
            }
            Console.WriteLine("Aaand the results are:");
            Console.WriteLine(AllTours.Aggregate((x,y)=> (x)+System.Environment.NewLine+(y)));
            Console.ReadKey();
        }

        public static string doRoute(City[] cities,string cityName){
            Console.WriteLine($"===========StarterCity[{cityName}]========================");
            var Tour = new Tour(cities.FirstOrDefault(x => x.Name == cityName));
            foreach(var city in cities){
                Tour.addCity(city);
            }
            var tour = Tour.findTour();
            Console.WriteLine($"Result for run [{tour}]");
            Console.WriteLine("============================================================");
            return tour;
        }
    }
}
