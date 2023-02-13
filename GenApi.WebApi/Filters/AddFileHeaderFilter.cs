using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Mime;

namespace GenApi.WebApi.Filters;

public class AddFileHeaderFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Controller is ControllerBase controller && controller.HttpContext.Items.ContainsKey(Constants.ApplicationName))
        {
            string appName = controller.HttpContext.Items[Constants.ApplicationName] as string;
            context.HttpContext.Response.Headers.Append("Content-Disposition", $"attachment; filename={appName}.zip");
            context.HttpContext.Response.Headers.Append("Content-Type", MediaTypeNames.Application.Zip);
        }
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
    }
}
