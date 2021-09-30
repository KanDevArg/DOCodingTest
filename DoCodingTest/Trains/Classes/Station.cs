using System.Collections.Generic;

namespace CandidateTest.TrainsRoutes
{
    public class Station
    {
        public string Name { get; set; }
        
        public Dictionary<string, int> Routes { get; set; }
    }
}