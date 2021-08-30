using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Exceptions
{
    public class GoodsNotFoundException : Exception
    {
        public GoodsNotFoundException(string message) : base(message)
        {
        }

    }
}
