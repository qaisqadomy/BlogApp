namespace Domain.Exeptions;

/// <summary>
/// Represents an exception that is thrown when an invalid token is encountered.
/// </summary>
public class InvalidToken(string msg) : InvalidOperation(msg)
{
}
