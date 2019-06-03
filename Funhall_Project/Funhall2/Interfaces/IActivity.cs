using System;

namespace Funhall2
{
    public interface IActivity
    {
        string BookingId { get; set; }
        DateTime EndTime { get; set; }
        bool IsFinished { get; set; }
        DateTime StartTime { get; set; }
        string TimeDesc { get; set; }
    }
}