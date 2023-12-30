using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHotel.Infrastructure.Services
{
    public interface ICurrentUserService
    {
        IUserSession GetCurrentUser();
    }
}
