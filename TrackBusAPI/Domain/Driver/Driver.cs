using Domain.Primitives;

namespace Domain.DriverAggregate;

public class Driver : User
{
    public int CompanyId { get; private set; }

    private Driver() { }
    private Driver(string driverName, string driverPassword) :base( driverName, driverPassword )
    {
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
            Username = newDriverName;
    }

    public void UpdateDriverPassword(string newDriverPassword)
    {
        if (string.IsNullOrWhiteSpace(newDriverPassword))
            throw new ArgumentException("Driver password cannot be empty.", nameof(newDriverPassword));
            Password = newDriverPassword;
    }

}
