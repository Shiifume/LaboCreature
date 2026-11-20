using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace LaboCreature.BLL.Exceptions
{
    public class PlayerException : Exception
    {
        public PlayerException() : base() { }

        public PlayerException(string message) : base(message) { }
    }

    public class PlayerNotFoundException : PlayerException
    {
        public PlayerNotFoundException() : base() { }
        public PlayerNotFoundException(string message) : base(message) { }
    }

    public class PlayerNotEnoughMoneyException : PlayerException
    {
        public PlayerNotEnoughMoneyException() : base() { }
        public PlayerNotEnoughMoneyException(string message) : base(message) { }
    }

    public class PlayerMoneyLimitException : PlayerException
    {
        public PlayerMoneyLimitException() : base() { }
        public PlayerMoneyLimitException(string message) : base(message) { }
    }

    public class PlayerAlreadyInMember : PlayerException
    {
        public PlayerAlreadyInMember() : base() { }
        public PlayerAlreadyInMember(string message) : base(message) { }
    }

    public class PlayerHasReachedMaxCreatures : PlayerException
    {
        public PlayerHasReachedMaxCreatures() : base() { }
        public PlayerHasReachedMaxCreatures(string message) : base(message) { }
    }
}
