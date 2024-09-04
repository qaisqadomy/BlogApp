namespace Domain.Exeptions;

/// <summary>
/// Represents an exception that is thrown when a comment cannot be found.
/// </summary>

public class CommentNotFound(string msg) : NotFound(msg)
{
}
