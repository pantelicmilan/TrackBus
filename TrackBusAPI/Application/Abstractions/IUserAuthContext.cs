using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions;

public interface IUserAuthContext
{
    public int Id { get; }
    public string UserName{ get; }
    public Role Role { get; }
}
