using ePizza.Models.Response;
using System.Text.Json;

namespace ePizza.API.Middlewares
{
    public class CommonResponseMiddlewares
    {
        private readonly RequestDelegate _next;  // to convert constructor to middleware 
        public CommonResponseMiddlewares(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var OriginalBodyStream=context.Response.Body;
            using (var MemmoryStraeam = new MemoryStream())
            { 
            context.Response.Body = MemmoryStraeam;
                try
                {
                    await _next(context);
                    if (context.Response.ContentType != null
                        && context.Response.ContentType.Contains("application/json"))
                    { 
                    MemmoryStraeam.Seek(0, SeekOrigin.Begin);
                        var ResponseBody= await new StreamReader(MemmoryStraeam).ReadToEndAsync();
                        var Responseobj =
                            new ApiResponseModel<object>
                            (Success: context.Response.StatusCode >= 200 && context.Response.StatusCode <= 300,
                            data: JsonSerializer.Deserialize<object>(ResponseBody)!,
                            Message: "Request Completd Successfully");

                        var jsonrespose=JsonSerializer.Serialize(Responseobj);
                        context.Response.Body=OriginalBodyStream;
                        await context.Response.WriteAsync(jsonrespose);
                    }

                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    var errorResponse = new
                    {
                        success = false,
                        data = (object)null,
                        message = ex.Message
                    };
                    
                    var jsonrespose = JsonSerializer.Serialize(errorResponse);
                    context.Response.Body = OriginalBodyStream;
                    await context.Response.WriteAsync(jsonrespose);
                }
            }

                
        }
    }
}
