using BusinessLogic.Result;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Extensions;

public static class ControllerExtensions
{
    public static ActionResult FromResult<T>(this ControllerBase controller, Result<T> result)
    {
        switch (result.ResultType)
        {
            case ResultType.Ok:
                return controller.Ok(result.Data);
            case ResultType.NoContent:
                return controller.Ok("Operation completed satisfactorily");
            case ResultType.NotFound:
                return controller.NotFound(result.Errors);
            case ResultType.Invalid:
                return controller.BadRequest(result.Errors);
            case ResultType.Unexpected:
                return controller.BadRequest(result.Errors);
            case ResultType.Unauthorized:
                return controller.Unauthorized(result.Errors);
            default:
                throw new Exception("An unhandled result has occurred as a result of a service call.");
        }
    }
}
