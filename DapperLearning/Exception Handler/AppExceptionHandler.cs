using Microsoft.AspNetCore.Diagnostics;

namespace DapperLearning.Exception_Handler
{
    public class AppExceptionHandler : IExceptionHandler
    {
        async ValueTask<bool> IExceptionHandler.TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var error = new ErrorResponse
            {
                StatusCode = 500,
                ErrorMessage = exception.Message,
                Title = "Something went weong"
            };

             httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(error, cancellationToken);

            return true;
        }
    }
}
