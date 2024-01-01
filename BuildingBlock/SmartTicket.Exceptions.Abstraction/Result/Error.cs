using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHotel.Abstraction.Result
{
    public sealed record Error(string Code, string Description)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
    }
}
