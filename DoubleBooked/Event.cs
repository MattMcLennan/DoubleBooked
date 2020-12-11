using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DoubleBooked.Tests.Unit")]
namespace DoubleBooked
{
    class Event
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Title { get; set; }

        public Event(string title, DateTime startTime, DateTime endTime)
        {
            Title = title; 
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
