using WebbApiwithCQS.Domain.Abstractions.Errors;

namespace WebbApiwithCQS.Domain.Abstractions.Results
{
    public sealed class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        private Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
                throw new InvalidOperationException("A result cannot be successful and contain an error.");
            // throw new ArgumentException("Invalid operation: ", nameof(error));

            if (!isSuccess && error == Error.None)
                throw new InvalidOperationException("A result cannot be failure and not contain an error.");

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true, Error.None);

        public static Result Failure(Error error)
        {
            if (string.IsNullOrWhiteSpace(error.Code))
                throw new ArgumentException(nameof(error.Code), "Invalid Code");

            if (string.IsNullOrWhiteSpace(error.Message))
                throw new ArgumentException(nameof(error.Message), "Invalid Message");

            return new Result(false, error);
        }

        public static implicit operator Result(Error error) => Failure(error);

        public static implicit operator Result(Exception ex) => Failure(Error.Exception);
    }

    public sealed class Result<TResult>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }
        public TResult Data { get; }

        private Result(bool isSucces, Error error, TResult data = default!)
        {
            IsSuccess = isSucces;
            Error = error;
            Data = data;
        }

        public static Result<TResult> Success(TResult data)
        {
            if (data is null)
                return new Result<TResult>(false, Error.Null);

            return new Result<TResult>(true, Error.None, data);
        }

        public static Result<TResult> Failure(Error error)
        {
            if (string.IsNullOrWhiteSpace(error.Code))
                throw new ArgumentException(nameof(error.Code), "Invalid Code");

            if (string.IsNullOrWhiteSpace(error.Message))
                throw new ArgumentException(nameof(error.Message), "Invalid Message");

            return new Result<TResult>(false, error);
        }

        public static implicit operator Result<TResult>(Error error) => Failure(error);

        public static implicit operator Result<TResult>(Exception ex) => Failure(Error.Exception);
        public static implicit operator Result<TResult>(TResult data) => Success(data);

    }
}
