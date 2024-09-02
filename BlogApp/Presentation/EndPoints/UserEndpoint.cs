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
    /// <param name="app">The <see cref="WebApplication"/> instance to map the endpoints on.</param>
    public static void MapUserEndpoint(this WebApplication app)
    {
        RouteGroupBuilder UserGroup = app.MapGroup("/user")
            .WithTags("users");

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="model">The <see cref="UserDTO"/> representing the user to register.</param>
        /// <param name="service">The <see cref="UserService"/> instance used to register the user.</param>
        /// <returns>An HTTP response indicating the result of the registration operation.</returns>
        UserGroup.MapPost("/register", (UserDTO model, UserService service) =>
        {
            service.Register(model);
            return Results.Ok();
        });

        /// <summary>
        /// Logs in a user and returns a token.
        /// </summary>
        /// <param name="Email">The email of the user trying to log in.</param>
        /// <param name="Password">The password of the user trying to log in.</param>
        /// <param name="service">The <see cref="UserService"/> instance used to authenticate the user.</param>
        /// <returns>An HTTP response containing the authentication token if the login was successful.</returns>
        UserGroup.MapPost("/login", (string Email, string Password, UserService service) =>
        {
            string token = service.Login(Email, Password);
            return Results.Ok(token);
        });

        /// <summary>
        /// Gets the details of the currently authenticated user.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> for the current request.</param>
        /// <param name="service">The <see cref="UserService"/> instance used to get the user details.</param>
        /// <returns>An HTTP response containing the user details if the token is valid, otherwise an unauthorized response.</returns>
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
        /// <param name="context">The <see cref="HttpContext"/> for the current request.</param>
        /// <param name="user">The <see cref="UserDTO"/> containing the updated user details.</param>
        /// <param name="service">The <see cref="UserService"/> instance used to update the user details.</param>
        /// <returns>An HTTP response indicating the result of the update operation.</returns>
        UserGroup.MapPut("/", (HttpContext context, UserDTO user, UserService service) =>
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();
            var token = authHeader["Bearer ".Length..].Trim();
            service.Update(user, token);
            return Results.Ok(user);
        });
    }
}
