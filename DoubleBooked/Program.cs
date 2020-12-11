using Newtonsoft.Json;
using System;

namespace DoubleBooked
{
    class Program
    {
        static void Main(string[] args)
        {

            var scheduler = new EventScheduler();

            scheduler.AddEvent("Testing1", DateTime.Now.AddHours(0), DateTime.Now.AddHours(5));
            scheduler.AddEvent("Testing2", DateTime.Now.AddHours(0), DateTime.Now.AddHours(10));
            scheduler.AddEvent("Testing3", DateTime.Now.AddHours(2), DateTime.Now.AddHours(10));
            scheduler.AddEvent("Testing4", DateTime.Now.AddHours(30), DateTime.Now.AddHours(50));
            scheduler.AddEvent("Testing5", DateTime.Now.AddHours(50), DateTime.Now.AddHours(70));
            scheduler.AddEvent("Testing6", DateTime.Now.AddHours(70), DateTime.Now.AddHours(90));
            scheduler.AddEvent("Testing7", DateTime.Now.AddHours(90), DateTime.Now.AddHours(90));
            scheduler.AddEvent("Testing8", DateTime.Now.AddHours(10), DateTime.Now.AddHours(20));
            scheduler.AddEvent("Testing9", DateTime.Now.AddHours(91), DateTime.Now.AddHours(91));
            scheduler.AddEvent("Testing10", DateTime.Now.AddHours(100), DateTime.Now.AddHours(105));
            scheduler.AddEvent("Testing11", DateTime.Now.AddHours(100), DateTime.Now.AddHours(104));

            var conflicts = scheduler.FindConflicts();

            Console.WriteLine("============ Conflicts ======================");
            Console.WriteLine();
            Console.WriteLine(JsonConvert.SerializeObject(conflicts, Formatting.Indented));
        }
    }
}
