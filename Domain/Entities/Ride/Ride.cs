using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Ride;

public class Ride : AggregateRoot
{
    public string RideName { get; private set; }
    public bool IsDailyRide { get; private set; }
    public int CompanyId { get; private set; }
    public RideDateRange RideDateRange { get; private set; }

    private readonly List<AvailableRideDay> _availableRideDays = new();
    public IReadOnlyCollection<AvailableRideDay> AvailableRideDays => _availableRideDays.AsReadOnly();

    public Ride(string rideName, bool isDailyRide, int companyId, RideDateRange rideDateRange ) 
    {
        RideName = rideName;
        IsDailyRide = isDailyRide;
        CompanyId = companyId;
        RideDateRange = rideDateRange;
    }

}
