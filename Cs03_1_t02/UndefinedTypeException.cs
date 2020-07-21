using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hw
{
    class UndefinedTypeException : Exception
    {
        public UndefinedTypeException(string message) : base(message) { }
    }
}
