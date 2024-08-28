using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.EndPoints;

public static class UserEndpoint
{

  public static void MapUserEndpoint(this WebApplication app)
  {
    RouteGroupBuilder UserGroup = app.MapGroup("/user")
      .WithTags("users");


    UserGroup.MapPost("/register", (UserDTO model, UserService service) =>
    {
      service.Register(model);
      return Results.Ok();
    });
    UserGroup.MapPost("/login", (string Email, string Password, UserService service) =>
  {
    string token = service.Login(Email, Password);
    return Results.Ok(token);
  });

    UserGroup.MapGet("/", [Authorize] (HttpContext context, UserService service) =>
 {
   var authHeader = context.Request.Headers["Authorization"].ToString();

   if (authHeader != null && authHeader.StartsWith("Bearer "))
   {
     var token = authHeader.Substring("Bearer ".Length).Trim();
     GetUserDTO user = service.Get(token);
     return Results.Ok(user);
   }
   else
   {
     return Results.Unauthorized();
   }
 });
    UserGroup.MapPut("/", [Authorize] (HttpContext context, UserDTO user, UserService service) =>
 {
   var authHeader = context.Request.Headers["Authorization"].ToString();

   if (authHeader != null && authHeader.StartsWith("Bearer "))
   {
     var token = authHeader.Substring("Bearer ".Length).Trim();
     service.Update(user, token);
     return Results.Ok(user);
   }
   else
   {
     return Results.Unauthorized();
   }
 });
  }
}
