using System;
using System.Collections.Generic;
using System.Linq;

namespace tourApp.Model
{
    public class Tour
    {

        public Tour(City starterCity)
        {
            this.StarterCity = starterCity;
            this.CurrentCity = starterCity;
        }

        private List<City> cities = new List<City>();
        private List<City> visitedCities = new List<City>();
        private double DistanceTraveled { get; set; } = 0d;
        private City StarterCity { get; }
        private City CurrentCity { get; set; }

        public string Route { get; private set; }

        private void AddStepToRoute(City step)
        {
            if (!visitedCities.Exists(x => x.Name == step.Name))
            {
                visitedCities.Add(step);
            }

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
            CurrentCity = city;
            AddStepToRoute(city);
            var nextCity = FindNextCity(city);
            if (nextCity == null)
            {
                //didnt find a next city.
                return;
            }
            GoToNextCity(nextCity);
        }

        private void GoHome()
        {
            Console.WriteLine($"Going home to {StarterCity.Name}");
            foreach(var distance in CurrentCity.Distances)
            {
                if ( distance.City == StarterCity.Name)
                {
                    DistanceTraveled += distance.Amount;
                    AddStepToRoute(StarterCity);
                }
            }
        }

        private City FindNextCity(City city)
        {
            Console.WriteLine("Find next city from " + city.Name);

            var availableDistances = cities.Except(visitedCities).ToList(); //diff between two arrays

            string nameForNextCity ="";
            double lengthToNextCity = double.MaxValue;

            foreach (var dist in availableDistances)
            {
                var currentDist = city.distanceTo(dist);
                if (currentDist < lengthToNextCity)
                {
                    nameForNextCity = dist.Name;
                    lengthToNextCity = currentDist;
                };
            }

            City next = null;
            foreach ( City c in cities)
            //for(int i = 0; i< cities.Count)
            {
                if (c.Name == nameForNextCity)
                    next = c;
            }

            if (next != null)
            {
                DistanceTraveled += lengthToNextCity;
                Console.WriteLine($"Found {next.Name} with amount {lengthToNextCity}");
            }
            return next;
        }

        public string findTour()
        {
            Console.WriteLine($"Starting simulation with starterCity[{StarterCity.Name}]");
            while (visitedCities.Count < cities.Count)
            {
                GoToNextCity(StarterCity);
            }
            GoHome();
            return Route + $" with an total distance traveled: {DistanceTraveled}";
        }
    }
}