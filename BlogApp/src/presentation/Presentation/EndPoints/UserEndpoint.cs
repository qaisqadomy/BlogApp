using Application.DTOs;
using Application.Services;

namespace Presentation.EndPoints;

/// <summary>
/// Contains the endpoint mappings for user-related operations.
/// </summary>
public static class UserEndpoints
{
    /// <summary>
    /// Maps the user-related endpoints to the specified <see cref="WebApplication"/> instance.
    /// </summary>

    public static void MapUserEndpoint(this WebApplication app)
    {
        RouteGroupBuilder UserGroup = app.MapGroup("/user")
            .WithTags("users");

        /// <summary>
        /// Registers a new user.
        /// </summary>

        UserGroup.MapPost("/register", (UserDTO model, UserService service) =>
        {
            service.Register(model);
            return Results.Ok();
        });

        /// <summary>
        /// Logs in a user and returns a token.
        /// </summary>
        
        UserGroup.MapPost("/login", (string Email, string Password, UserService service) =>
        {
            string token = service.Login(Email, Password);
            return Results.Ok(token);
        });

        /// <summary>
        /// Gets the details of the currently authenticated user.
        /// </summary>
 
        UserGroup.MapGet("/", (HttpContext context, UserService service) =>
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();

            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader["Bearer ".Length..].Trim();
                GetUserDTO user = service.Get(token);
                return Results.Ok(user);
            }
            else
            {
                return Results.Unauthorized();
            }
        });

        /// <summary>
        /// Updates the details of the currently authenticated user.
        /// </summary>
       
        UserGroup.MapPut("/", (HttpContext context, UserDTO user, UserService service) =>
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();
            var token = authHeader["Bearer ".Length..].Trim();
            service.Update(user, token);
            return Results.Ok(user);
        });
    }
}
