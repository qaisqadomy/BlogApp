namespace Domain.Exeptions;

/// <summary>
/// Represents an exception that is thrown when a user is not found.
/// </summary>

public class UserNotFound(string msg) : NotFound(msg)
{
}
