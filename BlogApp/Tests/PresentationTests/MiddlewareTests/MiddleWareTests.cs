using Microsoft.AspNetCore.Http;
using Moq;
using Presentation.Middleware;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Domain.Exeptions;

namespace PresentationTests.MiddlewareTests;

public class MiddleWareTests
{
    private readonly Mock<ILogger<ExceptionHandlingMiddleware>> _mockLogger;

    public MiddleWareTests()
    {
        _mockLogger = new Mock<ILogger<ExceptionHandlingMiddleware>>();
    }

    private async Task TestExceptionHandlingMiddlewareAsync(
         RequestDelegate requestDelegate,
         Func<HttpContext, Task> assertAction)
    {
        var context = new DefaultHttpContext();
        var middleware = new ExceptionHandlingMiddleware(requestDelegate, _mockLogger.Object);


        var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        await middleware.InvokeAsync(context);


        responseBodyStream.Seek(0, SeekOrigin.Begin);
        await assertAction(context);
    }
    [Fact]
    public async Task Handle_SuccessfulRequest_Returns200AndNoContent()
    {
        RequestDelegate requestDelegate = async context =>
 {

     context.Response.StatusCode = StatusCodes.Status200OK;
     context.Response.ContentType = "text/plain";
     await context.Response.WriteAsync("Success");
 };


        var context = new DefaultHttpContext();
        var middleware = new ExceptionHandlingMiddleware(requestDelegate, _mockLogger.Object);


        var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        await middleware.InvokeAsync(context);


        responseBodyStream.Seek(0, SeekOrigin.Begin);


        Assert.Equal(StatusCodes.Status200OK, context.Response.StatusCode);
        Assert.Equal("text/plain", context.Response.ContentType);

        var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        Assert.Equal("Success", responseBody);
    }

    [Fact]
    public async Task Handle_NotFoundException_Returns404AndJson()
    {

        Task RequestDelegate(HttpContext context)
        {
            throw new NotFound("Resource not found");
        }


        await TestExceptionHandlingMiddlewareAsync(
            RequestDelegate,
            async context =>
            {
                Assert.Equal(StatusCodes.Status404NotFound, context.Response.StatusCode);
                Assert.Equal("application/json", context.Response.ContentType);

                var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
                var errorResponse = JsonSerializer.Deserialize<JsonElement>(responseBody);

                Assert.True(errorResponse.TryGetProperty("Message", out var message));
                Assert.Equal("Resource not found", message.GetString());
            }
        );
    }

    [Fact]
    public async Task Handle_InvalidOperationException_Returns400AndJson()
    {

        Task RequestDelegate(HttpContext context)
        {
            throw new InvalidOperationException("Invalid operation");
        }


        await TestExceptionHandlingMiddlewareAsync(
            RequestDelegate,
            async context =>
            {
                Assert.Equal(StatusCodes.Status400BadRequest, context.Response.StatusCode);
                Assert.Equal("application/json", context.Response.ContentType);

                var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
                var errorResponse = JsonSerializer.Deserialize<JsonElement>(responseBody);

                Assert.True(errorResponse.TryGetProperty("Message", out var message));
                Assert.Equal("Invalid operation", message.GetString());
            }
        );
    }

    [Fact]
    public async Task Handle_BadHttpRequestException_Returns400AndJson()
    {

        Task RequestDelegate(HttpContext context)
        {
            throw new BadHttpRequestException("Invalid request data");
        }


        await TestExceptionHandlingMiddlewareAsync(
            RequestDelegate,
            async context =>
            {
                Assert.Equal(StatusCodes.Status400BadRequest, context.Response.StatusCode);
                Assert.Equal("application/json", context.Response.ContentType);

                var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
                var errorResponse = JsonSerializer.Deserialize<JsonElement>(responseBody);

                Assert.True(errorResponse.TryGetProperty("Message", out var message));
                Assert.Equal("Invalid request data.", errorResponse.GetProperty("Message").GetString());

                Assert.True(errorResponse.TryGetProperty("Details", out var details));
                Assert.Equal("Invalid request data", details.GetString());
            }
        );
    }
    [Fact]
    public async Task Handle_Exception_Returns500AndJson()
    {

        static Task requestDelegate(HttpContext context)
        {
            throw new Exception("Unexpected error");
        }


        await TestExceptionHandlingMiddlewareAsync(
requestDelegate,
                async context =>
                {
                    Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
                    Assert.Equal("application/json", context.Response.ContentType);

                    var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
                    var errorResponse = JsonSerializer.Deserialize<JsonElement>(responseBody);

                    Assert.True(errorResponse.TryGetProperty("Message", out var message));
                    Assert.Equal("An unexpected error occurred.", message.GetString());
                }
            );
    }

}
