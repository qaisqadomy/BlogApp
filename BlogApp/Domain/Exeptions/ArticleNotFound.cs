namespace Domain.Exeptions;

/// <summary>
/// Represents an exception that is thrown when an article cannot be found.
/// </summary>

public class ArticleNotFound(string msg) : NotFound(msg)
{
}
