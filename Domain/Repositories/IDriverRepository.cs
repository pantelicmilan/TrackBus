using Domain.Entities.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;

public interface IDriverRepository
{
    public Task<Driver> GetDriverById(int driverId);
    public void CreateDriver(Driver driver);
    public void DeleteDriver(int driverId);
    public void UpdateDriver(Driver driver);
}
