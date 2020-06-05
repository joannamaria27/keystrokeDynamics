using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace keystrokeDynamics
{
    class Data
    {
        public string Person { get; private set; }
        public Dictionary<string, long> dwellTimes { get; private set; }
        public Dictionary<string, long> flightTime { get; private set; }

        public Data(string _Person, Dictionary<string, long> _DwellTimes, Dictionary<string, long> _FlightTime)
        {
            Person = _Person;
            dwellTimes = _DwellTimes;
            flightTime = _FlightTime;
        }
    }
   
}
