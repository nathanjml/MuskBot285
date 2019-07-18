using MuskBot.Helper;

namespace MuskBot.Core.Extensions
{
    public static class ResultExtensions
    {
        public static Result<T> AsResult<T>(this T data, bool successful = true)
        {
            return new Result<T>
            {
                Data = data,
                WasSuccessful = successful
            };
        }
    }
}
