using PratiBus.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Ride;

public class AvailableRideDay : Entity
{
    private AvailableRideDay(int dayOfWeek)
    {
        DayOfWeek = dayOfWeek;
    }

    public AvailableRideDay CreateAvailableRideDay(int dayOfWeek) 
    { 
        return new AvailableRideDay(dayOfWeek);
    }

    public int DayOfWeek { get; private set; }
    public int RideId { get; private set; }  = 0;
}
