using System;

namespace SmartHotel.Exceptions.Abstraction
{
    public abstract class DomainException : Exception
    {
        public int ExceptionCode { get; }
        public DomainException(string message, int exceptionCode) : base(message)
        {
            ExceptionCode = exceptionCode;
        }
    }
}