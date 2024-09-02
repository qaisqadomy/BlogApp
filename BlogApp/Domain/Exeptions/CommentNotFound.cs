namespace Domain.Exeptions;

/// <summary>
/// Represents an exception that is thrown when a comment cannot be found.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="CommentNotFound"/> class with a specified error message.
/// </remarks>
/// <param name="msg">The message that describes the error.</param>
public class CommentNotFound(string msg) : NotFound(msg)
{
}
