using WebbApiwithCQS.Domain.Abstractions.Results;

namespace WebbApiwithCQS.Domain.Abstractions.Errors
{
    public sealed record Error(string Code, string? Message = null)
    {
        public static readonly Error None = new Error(string.Empty);
        //internal static Error None => new Error("", "");
        internal static Error Exception => new Error("Exception", "An unexpected error occurred (see logs for details).");
        internal static Error Null => new Error("Error.Null", "The NULL value was received as null.");

        public static implicit operator Result(Error error) => Result.Failure(error);
    }
}
