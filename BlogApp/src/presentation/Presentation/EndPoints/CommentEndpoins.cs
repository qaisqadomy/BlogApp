using Application.DTOs;
using Application.Services;

namespace Presentation.EndPoints;

/// <summary>
/// Contains the endpoint mappings for comment-related operations.
/// </summary>
public static class CommentEndpoints
{
    /// <summary>
    /// Maps the comment-related endpoints to the specified <see cref="WebApplication"/> instance.
    /// </summary>
    public static void MapCommentEndpoint(this WebApplication app)
    {
        RouteGroupBuilder CommentGroup = app.MapGroup("/comment")
            .WithTags("comments");

        /// <summary>
        /// Gets a list of all comments.
        /// </summary>
        CommentGroup.MapGet("/", (CommentService service) =>
        {
            List<CommentViewDTO> list = service.GetAll();
            return Results.Ok(list);
        });

        /// <summary>
        /// Adds a new comment.
        /// </summary>
        CommentGroup.MapPost("/", (CommentDTO model, CommentService service) =>
        {
            service.AddComment(model);
            return Results.Ok();
        });

        /// <summary>
        /// Deletes a comment by its ID.
        /// </summary>
        CommentGroup.MapDelete("/{id}", (int id, CommentService service) =>
        {
            service.DeleteComment(id);
            return Results.NoContent();
        });
    }
}
