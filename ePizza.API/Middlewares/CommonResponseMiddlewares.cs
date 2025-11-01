using ePizza.Models.Response;
using System.IO;
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
            var originalBodyStream=context.Response.Body;
            using (var memmoryStraeam = new MemoryStream())
            { 
            context.Response.Body = memmoryStraeam;
                try
                {
                        await _next(context);

                    if (context.Response.ContentType != null
                        && context.Response.ContentType.Contains("application/json"))  // only process if response is in json format
                    {
                        memmoryStraeam.Seek(0, SeekOrigin.Begin);   // start reading memory stream
                        bool isSuccess = context.Response.StatusCode == 200
                            || context.Response.StatusCode == 201
                            || context.Response.StatusCode == 202
                            || context.Response.StatusCode == 204;

                        var responseBody = await new StreamReader(memmoryStraeam).ReadToEndAsync(); // read the response

                        // create response object
                        var repsonseObj
                            = new ApiResponseModel<object>(
                                 // success: context.Response.StatusCode >= 200 && context.Response.StatusCode <= 300,
                                 success: isSuccess,
                                  data: JsonSerializer.Deserialize<object>(responseBody)!,
                                  message: "Request completed successfully"
                                );

                        var jsonResponse = JsonSerializer.Serialize(repsonseObj);
                        context.Response.Body = originalBodyStream;
                        await context.Response.WriteAsync(jsonResponse); // send respone back to user
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
                    context.Response.Body = originalBodyStream;
                    await context.Response.WriteAsync(jsonrespose);
                }
            }

                
        }
    }
}
