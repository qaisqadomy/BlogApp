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
    /// <param name="app">The <see cref="WebApplication"/> instance to map the endpoints on.</param>
    public static void MapCommentEndpoint(this WebApplication app)
    {
        RouteGroupBuilder CommentGroup = app.MapGroup("/comment")
            .WithTags("comments");

        /// <summary>
        /// Gets a list of all comments.
        /// </summary>
        /// <param name="service">The <see cref="CommentService"/> instance used to get the comments.</param>
        /// <returns>A list of <see cref="CommentViewDTO"/> representing all comments.</returns>
        CommentGroup.MapGet("/", (CommentService service) =>
        {
            List<CommentViewDTO> list = service.GetAll();
            return Results.Ok(list);
        });

        /// <summary>
        /// Adds a new comment.
        /// </summary>
        /// <param name="model">The <see cref="CommentDTO"/> representing the comment to add.</param>
        /// <param name="service">The <see cref="CommentService"/> instance used to add the comment.</param>
        /// <returns>An HTTP response indicating the result of the add operation.</returns>
        CommentGroup.MapPost("/", (CommentDTO model, CommentService service) =>
        {
            service.AddComment(model);
            return Results.Ok();
        });

        /// <summary>
        /// Deletes a comment by its ID.
        /// </summary>
        /// <param name="id">The ID of the comment to delete.</param>
        /// <param name="service">The <see cref="CommentService"/> instance used to delete the comment.</param>
        /// <returns>An HTTP response with no content if the deletion was successful.</returns>
        CommentGroup.MapDelete("/{id}", (int id, CommentService service) =>
        {
            service.DeleteComment(id);
            return Results.NoContent();
        });
    }
}
