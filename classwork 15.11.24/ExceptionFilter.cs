using Microsoft.AspNetCore.Mvc.Filters;

namespace classwork_15._11._24
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = context.Exception switch
            {
                ArgumentException => 400,
                NullReferenceException => 404,
                _ => 500
            };

            context.ExceptionHandled = true;
        }
    }
}
