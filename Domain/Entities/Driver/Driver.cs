using Domain.Primitives;

namespace Domain.Entities.Driver;

public class Driver : AggregateRoot
{
    public int CompanyId { get; private set; }
    public string DriverName { get; private set; }
    public string DriverPassword { get; private set; }

    private Driver(string driverName, string driverPassword)
    {
        DriverName = driverName;
        DriverPassword = driverPassword;
    }

    public static Driver CreateDriver(string driverName, string driverPassword)
    {
        //validacija
        return new Driver(driverName, driverPassword);
    }

    public void UpdateDriverName(string newDriverName)
    {
        if (string.IsNullOrWhiteSpace(newDriverName))
            throw new ArgumentException("Driver name cannot be empty.", nameof(newDriverName));
            DriverName = newDriverName;
    }

    public void UpdateDriverPassword(string newDriverPassword)
    {
        if (string.IsNullOrWhiteSpace(newDriverPassword))
            throw new ArgumentException("Driver password cannot be empty.", nameof(newDriverPassword));
            DriverPassword = newDriverPassword;
    }

}
