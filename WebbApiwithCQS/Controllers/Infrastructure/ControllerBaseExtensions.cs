using Microsoft.AspNetCore.Mvc;
using WebbApiwithCQS.Domain.Abstractions.Results;

namespace WebbApiwithCQS.Controllers.Infrastructure
{
    public static class ControllerBaseExtensions
    {
        extension(ControllerBase controller)
        {
            public IActionResult FromResult(Result result)
            {
                if (result.IsFailure)
                    return controller.BadRequest(result.Error);

                return controller.NoContent();
            }

            public IActionResult FromResult<TResult>(Result<TResult> result)
            {
                Type[] types =
                [
                    typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(Guid)
                ];

                if (result.IsFailure)
                    return controller.BadRequest(result.Error);

                if (types.Contains(result.Data!.GetType()))
                    return controller.Ok(new { Id = result.Data });

                return controller.Ok(result.Data);
            }
        }
    }
}
