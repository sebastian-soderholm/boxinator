using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models
{
    public enum AccountTypes
    {
        GUEST = 1,
        REGISTERED_USER = 2,
        ADMINISTRATOR = 3
    }

    public enum StatusCodes
    {
        CREATED = 1,
        RECEIVED = 2,
        INTRANSIT = 3,
        COMPELED = 4,
        CANCELLED = 5
    }
}
