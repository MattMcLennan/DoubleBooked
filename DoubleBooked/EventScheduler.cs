using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DoubleBooked.Tests.Unit")]
namespace DoubleBooked
{
    class EventScheduler
    {
        public List<Event> Events { get; set; }
        public EventScheduler()
        {
            Events = new List<Event>();
        }

        public bool AddEvent(string title, DateTime startTime, DateTime endTime)
        {
            if (string.IsNullOrEmpty(title))
                return false;

            if (startTime > endTime)
                return false;

            var ev = new Event(title, startTime, endTime);
            Events.Add(ev);
            return true;
        }

        public List<Event> FindConflicts () {
            var conflicts = new List<Event> ();
            if (Events == null || Events.Count < 2)
                return conflicts;

            Events.Sort(new EventSort());
            var tempConflicts = new List<Event> ();

            DateTime maxEndTime = Events.First().EndTime;
            tempConflicts.Add (Events.First());

            foreach (var ev in Events.Skip(1))
            {
                if (ev.StartTime >= maxEndTime) {
                    if (tempConflicts.Count > 1)
                        conflicts.AddRange (tempConflicts);

                    tempConflicts.Clear ();
                }

                maxEndTime = ev.EndTime > maxEndTime ? ev.EndTime : maxEndTime;
                tempConflicts.Add (ev);
            }

            if (tempConflicts.Count > 1)
                conflicts.AddRange (tempConflicts);

            return conflicts;
        }
    }
}
