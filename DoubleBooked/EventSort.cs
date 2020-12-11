using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DoubleBooked
{
    class EventSort : IComparer<Event>
    {
        public int Compare([AllowNull] Event x, [AllowNull] Event y)
        {
            var res = x.StartTime.CompareTo(y.StartTime);
            return res != 0 ? res : x.EndTime.CompareTo(y.EndTime);
        }
    }
}
