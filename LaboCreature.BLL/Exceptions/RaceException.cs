using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.BLL.Exceptions
{
    public class RaceException : Exception
    {
        public RaceException() : base() { }
        public RaceException(string message) : base(message) { }
    }

    public class RaceNotFoundException : RaceException
    {
        public RaceNotFoundException() : base() { }
        public RaceNotFoundException(string message) : base(message) { }
    }
}
