using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.BLL.Exceptions
{
    public class MemberException : Exception
    {
        public MemberException() : base() { }
        public MemberException(string message) : base(message) { }
    }

    public class MemberEmailAlreadyExists : MemberException
    {
        public MemberEmailAlreadyExists() : base() { }
        public MemberEmailAlreadyExists(string message) : base(message) { }
    }
    public class MemberUsernameAlreadyExists : MemberException
    {
        public MemberUsernameAlreadyExists() : base() { }
        public MemberUsernameAlreadyExists(string message) : base(message) { }
    }
}
