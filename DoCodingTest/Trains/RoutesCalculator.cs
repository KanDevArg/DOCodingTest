using System;
using System.Collections.Generic;

namespace CandidateTest.TrainsRoutes
{ 
    public class RoutesCalculator
    {
        private Dictionary<string, Station> stations;
        private readonly List<string> trips;
        public RoutesCalculator()
        {
            trips = new List<string>();
            SetStations();
        }
        
        public string CalculateDistance(string path)
        {
            var totalDistance = DistanceCalculator(path);
            return totalDistance > 0 ? totalDistance.ToString() : "NO SUCH ROUTE";
        }
        private int DistanceCalculator(string path)
        {
            var stationNames = path.Split("-");
            var distance = 0;

            if (!stations.ContainsKey(stationNames[0]))
            {
                return 0;
            }
            
            for (var i = 0; i < stationNames.Length -1; i++)
            {
                var s = stations[stationNames[i]];
                if (s.Routes.ContainsKey(stationNames[i+1]))
                {
                    distance += s.Routes[stationNames[i+1]];
                }else
                {
                    return 0;
                }
            }

            return distance;
        }


        public int GetNumberOfTripsBetweenStations(string StationStart, string StationEnd, int maxStops, bool matchMaxStops)
        {
            trips.Clear();
            NumberOfTripsCalculator(StationStart, StationEnd, maxStops, matchMaxStops, "");
            foreach (var path in trips)
            {
                Console.WriteLine($"Trip: {path}  TotalDistance:{DistanceCalculator(path)}");
            }
            Console.WriteLine("---------------------");
            return trips.Count;
        }
        
        public int GetAllPathsLimitByTotalDistance(string StationStart, string StationEnd, int totalDistanceLimit)
        {
            trips.Clear();
            NumberOfTripsCalculator(StationStart, StationEnd, "", totalDistanceLimit);
            foreach (var path in trips)
            {
                Console.WriteLine($"Trip: {path}  TotalDistance:{DistanceCalculator(path)}");
            }
            Console.WriteLine("---------------------");
            return trips.Count;
        }

        private void NumberOfTripsCalculator(string StationStart, string StationEnd, string piggybackPath, int totalDistanceLimit)
        {
            var trimmedPiggyBackPath = piggybackPath.TrimEnd('-');
            var arr = trimmedPiggyBackPath.Split("-");
            var distance = DistanceCalculator(trimmedPiggyBackPath);
            
            if (distance > totalDistanceLimit) return;
                    
            if (arr.Length >1  && arr[^1] == StationEnd &&  distance < totalDistanceLimit) {
                if (!trips.Contains(trimmedPiggyBackPath)) {
                    trips.Add(trimmedPiggyBackPath);    
                }
            }
            
            var start = stations[StationStart];
            if (start.Routes.Count <= 0) return;
            
            foreach (var route in start.Routes) {
                NumberOfTripsCalculator(route.Key, StationEnd, piggybackPath.Length == 0 ?  start.Name + "-" : piggybackPath + start.Name + "-", totalDistanceLimit);
            }
        }
        
        private void NumberOfTripsCalculator(string StationStart, string StationEnd, int maxStops, bool matchMaxStops, string piggybackPath)
        {
            var trimmedPiggyBackPath = piggybackPath.TrimEnd('-');
            var arr = trimmedPiggyBackPath.Split("-");

            if (arr.Length > 2) {
                if (arr.Length > maxStops + 2) return;
                
                if (arr[^1] == StationEnd && (matchMaxStops && arr.Length == maxStops + 1 || !matchMaxStops && arr.Length <= maxStops + 1)) {
                    if (!trips.Contains(trimmedPiggyBackPath)) {
                        trips.Add(trimmedPiggyBackPath);
                    }
                    return;
                }
            }
            var start = stations[StationStart];

            if (start.Routes.Count <= 0) return;
            
            foreach (var route in start.Routes)
            {
                NumberOfTripsCalculator(route.Key, 
                    StationEnd, 
                    maxStops, 
                    matchMaxStops, 
                    piggybackPath.Length == 0 ?  start.Name + "-" : piggybackPath + start.Name + "-");
            }
            // Parallel.ForEach(start.Routes, route =>
            // {
            //     NumberOfTripsCalculator(route.Key, StationEnd, maxStops, matchMaxStops, piggyback.Length == 0 ?  start.Name + "-" : piggyback + start.Name + "-");
            // });
        }
        
        private void SetStations()
        {
            stations = new Dictionary<string, Station>
            {
                {"A", new Station() {Name = "A", Routes = new Dictionary<string, int>() {{"B", 5}, {"D", 5}, {"E", 7},}}},
                {"B", new Station() {Name = "B", Routes = new Dictionary<string, int>() {{"C", 4},}}},
                {"C", new Station() {Name = "C", Routes = new Dictionary<string, int>() {{"D", 8}, {"E", 2},}}},
                {"D", new Station() {Name = "D", Routes = new Dictionary<string, int>() {{"C", 8}, {"E", 6},}}},
                {"E", new Station() {Name = "E", Routes = new Dictionary<string, int>() {{"B", 3},}}}
            };
        }
    }
}