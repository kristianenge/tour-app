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
            var availableDistances =  cities.Except(visitedCities).ToList();
            var lowestYetTuple = (value:double.MaxValue,name:"");
            foreach(var dist in availableDistances){
                var currentDist = city.distanceTo(dist);
                if(currentDist < lowestYetTuple.value){
                    lowestYetTuple.value = currentDist;
                    lowestYetTuple.name = dist.Name;
                };
            }
            
            var next = cities.FirstOrDefault(x => x.Name == lowestYetTuple.name);

            if (next != null)
            {
                DistanceTraveled += lowestYetTuple.value;
                Console.WriteLine($"Found {next.Name} with amount {lowestYetTuple.value}");
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