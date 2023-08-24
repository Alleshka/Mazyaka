using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.Common
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;

        public string Error { get; set; }

        protected Result(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
        {
            return new Result(true, null);

        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(value, true, null);
        }

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default(T), false, message);
        } 
    }

    public class Result<T> : Result
    {
        internal Result(T value, bool isSuccess, string error) : base(isSuccess, error)
        {
            this.Value = value;
        }

        public T Value { get; }
    }
}
