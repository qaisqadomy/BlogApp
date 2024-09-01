using Application.DTOs;
using Application.Services;

namespace Presentation.EndPoints;

public static class CommentEndpoins
{
    public static void MapCommentEndpoint(this WebApplication app)
    {
        RouteGroupBuilder CommentGroup = app.MapGroup("/comment")
             .WithTags("comments");


        CommentGroup.MapGet("/", (CommentService service) =>
{
    List<CommentViewDTO> list = service.GetAll();
    return Results.Ok(list);
});

        CommentGroup.MapPost("/", (CommentDTO model, CommentService service) =>
{
    service.AddComment(model);
    return Results.Ok();
});

        CommentGroup.MapDelete("/{id}", (int id, CommentService service) =>
 {
     service.DeleteComment(id);
     return Results.NoContent();
 });
    }
}