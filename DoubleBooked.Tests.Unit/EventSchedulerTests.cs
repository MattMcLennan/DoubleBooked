using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoubleBooked.Tests.Unit
{
    [TestClass]
    public class EventSchedulerTests
    {
        [TestMethod]
        public void AddEvent_StartDate_LessThan_End_Should_Return_True()
        {
            var scheduler = new EventScheduler();
            var result = scheduler.AddEvent("Test Title", DateTime.Now, DateTime.Now.AddHours(10));
            Assert.IsTrue(result);
            Assert.IsTrue(scheduler.Events.Count == 1);
        }

        [TestMethod]
        public void AddEvent_StartDate_GreaterThan_End_Should_Return_False()
        {
            var scheduler = new EventScheduler();
            var result = scheduler.AddEvent("Test Title", DateTime.Now.AddHours(10), DateTime.Now);
            Assert.IsFalse(result);
            Assert.IsFalse(scheduler.Events.Any());
        }

        [TestMethod]
        public void AddEvent_Missing_Title_Should_Return_False()
        {
            var scheduler = new EventScheduler();
            var result = scheduler.AddEvent("", DateTime.Now, DateTime.Now.AddHours(10));
            Assert.IsFalse(result);
            Assert.IsFalse(scheduler.Events.Any());
        }

        [TestMethod]
        public void FindConflicts_NoEvents_Should_Return_EmptyList()
        {
            var scheduler = new EventScheduler();
            var conflicts = scheduler.FindConflicts();
            Assert.IsFalse(conflicts.Any());
        }

        [TestMethod]
        public void FindConflicts_OneEvent_Should_Return_EmptyList()
        {
            var scheduler = new EventScheduler();
            scheduler.AddEvent("Test Title", DateTime.Now, DateTime.Now.AddHours(10));
            var conflicts = scheduler.FindConflicts();
            Assert.IsFalse(conflicts.Any());
        }

        [TestMethod]
        public void FindConflicts_MultipleEvents_NoConflicts_Should_Return_EmptyList()
        {
            var scheduler = new EventScheduler();

            scheduler.AddEvent("Testing1", DateTime.Now.AddHours(0), DateTime.Now.AddHours(10));
            scheduler.AddEvent("Testing2", DateTime.Now.AddHours(10), DateTime.Now.AddHours(20));
            scheduler.AddEvent("Testing3", DateTime.Now.AddHours(20), DateTime.Now.AddHours(29));
            scheduler.AddEvent("Testing4", DateTime.Now.AddHours(30), DateTime.Now.AddHours(40));
            scheduler.AddEvent("Testing5", DateTime.Now.AddHours(40), DateTime.Now.AddHours(50));
            scheduler.AddEvent("Testing6", DateTime.Now.AddHours(50), DateTime.Now.AddHours(60));
            scheduler.AddEvent("Testing7", DateTime.Now.AddHours(60), DateTime.Now.AddHours(70));
            scheduler.AddEvent("Testing8", DateTime.Now.AddHours(70), DateTime.Now.AddHours(80));
            scheduler.AddEvent("Testing9", DateTime.Now.AddHours(80), DateTime.Now.AddHours(90));
            scheduler.AddEvent("Testing10", DateTime.Now.AddHours(90), DateTime.Now.AddHours(90));

            var conflicts = scheduler.FindConflicts();
            Assert.IsFalse(conflicts.Any());
        }

        [TestMethod]
        public void FindConflicts_MultipleEvents_WithConflicts_Should_Return_ConflictList()
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

            var actualConflicts = scheduler.FindConflicts();

            var expectedConflicts = new List<string> { "Testing1", "Testing2", "Testing3", "Testing10", "Testing11" };
            Assert.IsTrue(actualConflicts.Select(i => i.Title).SequenceEqual(expectedConflicts));
        }
    }
}
