using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Exceptions
{
    public class ParamException : Exception
    {
        public ParamException(string message) : base(message)
        {
        }
    }
}
