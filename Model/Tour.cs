using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Tour
    {

        public Tour(City starterCity)
        {
            this.StarterCity = starterCity;
        }

        private List<City> cities = new List<City>();
        private List<City> visitedCities = new List<City>();
        private double DistanceTraveled { get; set; }
        private City StarterCity { get; }

        public string Route { get; private set; }

        private void AddStepToRoute(City step)
        {
            visitedCities.Add(step);
            if (string.IsNullOrEmpty(this.Route))
            {
                this.Route = step.Name;
            }
            else
            {
                this.Route += (" => " + step.Name);
            }
        }
        public void addCity(City c)
        {
            Console.WriteLine("adding city " + c.Name);
            this.cities.Add(c);
        }

        private void GoToNextCity(City city)
        {
            Console.WriteLine("GoToNextCity " + city.Name);
            AddStepToRoute(city);
            var nextCity = FindNextCity(city);
            if (nextCity == null)
            {
                //didnt find a next city.
                return;
            }
            GoToNextCity(nextCity);
        }

        private City FindNextCity(City city)
        {
            Console.WriteLine("Find next city from " + city.Name);
            bool foundMatch = false;
            var nextCityName = "";
            var distanceAmount = 0d;
            var availableDistances = city.Distances.OrderBy(x => x.Amount).ToList();
            while (!foundMatch && availableDistances.Count > 0)
            {
                var lowestDist = availableDistances.First();
                if (visitedCities.Exists(x => x.Name == lowestDist.City))
                {
                    Console.WriteLine($"Already visited {lowestDist.City}, removing it from possible and moving on to next..");
                    availableDistances.Remove(lowestDist);
                    continue;
                }
                nextCityName = lowestDist.City;
                distanceAmount = lowestDist.Amount;
                foundMatch = true;
            }
            var next = cities.FirstOrDefault(x => x.Name == nextCityName);

            if (next != null)
            {
                DistanceTraveled += distanceAmount;
                Console.WriteLine($"Found {next.Name} with amount {distanceAmount}");
            }
            return next;
        }

        public string findTour()
        {
            while (visitedCities.Count < cities.Count)
            {
                GoToNextCity(StarterCity);
            }
            return Route + $" with an total distance traveled: {DistanceTraveled}";
        }

    }
}