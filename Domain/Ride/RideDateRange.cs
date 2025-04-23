using PratiBus.Primitives;

namespace Domain.Ride;

// testovi da li radi createDateRange na ocekivani nacin (from to poredjenje)
//testovi za jednakost 2 ride date range value objekta, ili ti da li je Equals dobro izveden
public class RideDateRange : ValueObject
{
    public DateOnly From { get; private set; }
    public DateOnly To { get; private set; }


    private RideDateRange(DateOnly from, DateOnly to)
    {
        From = from;
        To = to;
    }

    public static RideDateRange CreateDateRange(DateOnly from, DateOnly to )
    {
        if (from > to)
        {
            throw new ArgumentException("From must be smaller than to");
        }

        return new RideDateRange(from, to);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        return new List<object>() { From, To};
    }
}
