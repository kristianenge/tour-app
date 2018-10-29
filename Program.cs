using System;
using Model;
using System.Linq;
using System.Collections.Generic;

namespace toure_app
{
    class Program
    {
        static void Main(string[] args)
        {
             var jsonFile = System.IO.File.ReadAllText(@"./data/cities.json");
            var cities = City.FromJson(jsonFile);
            
            var AllTours = new List<String>();
            foreach(var c in cities){
                AllTours.Add(doRoute(cities,c.Name));
            }
            Console.WriteLine("Aaand the results are:");
            Console.WriteLine(AllTours.Aggregate((x,y)=> (x)+System.Environment.NewLine+(y)));
        }

        public static string doRoute(City[] cities,string cityName){
            var Tour = new Tour(cities.FirstOrDefault(x => x.Name == cityName));
            foreach(var city in cities){
                Tour.addCity(city);
            }
            var tour = Tour.findTour();
            Console.WriteLine(tour);
            return tour;
        }
    }
}
