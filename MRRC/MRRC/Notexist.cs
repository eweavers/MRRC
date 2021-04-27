using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRRC
{
    public class DoesntExistException : Exception
    {
        public DoesntExistException(string message) : base(message)
        {
        }
    }
}
