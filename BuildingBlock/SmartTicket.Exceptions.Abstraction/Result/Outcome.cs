using System;

namespace SmartHotel.Abstraction.Result
{
    public class Outcome<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public Error Error { get; }

        private Outcome(bool isSuccess, T value, Error error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static Outcome<T> Success(T value)
        {
            return new Outcome<T>(true, value, null);
        }

        public static Outcome<T> Failure(Error error)
        {
            return new Outcome<T>(false, default(T), error);
        }
    }
}
