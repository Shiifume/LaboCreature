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

    public class MemberEmailAlreadyExistsException : MemberException
    {
        public MemberEmailAlreadyExistsException() : base() { }
        public MemberEmailAlreadyExistsException(string message) : base(message) { }
    }
    public class MemberUsernameAlreadyExistsException : MemberException
    {
        public MemberUsernameAlreadyExistsException() : base() { }
        public MemberUsernameAlreadyExistsException(string message) : base(message) { }
    }

    public class MemberNotFoundException : MemberException
    {
        public MemberNotFoundException() : base() { }
        public MemberNotFoundException(string message) : base(message){ }
    }

    public class MemberPasswordInvalidException : MemberException
    {
        public MemberPasswordInvalidException() : base() { }
        public MemberPasswordInvalidException(string message) : base(message) { }
    }
}
