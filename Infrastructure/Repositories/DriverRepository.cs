using Domain.Entities.Driver;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class DriverRepository : IDriverRepository
{
    public void CreateDriver(Driver driver)
    {
        throw new NotImplementedException();
    }

    public void DeleteDriver(int driverId)
    {
        throw new NotImplementedException();
    }

    public Task<Driver> GetDriverById(int driverId)
    {
        throw new NotImplementedException();
    }

    public void UpdateDriver(Driver driver)
    {
        throw new NotImplementedException();
    }
}
