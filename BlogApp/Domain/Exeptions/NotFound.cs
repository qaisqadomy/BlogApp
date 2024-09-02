namespace Domain.Exeptions;

/// <summary>
/// Represents an exception that is thrown when a requested entity is not found.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="NotFound"/> class with a specified error message.
/// </remarks>
/// <param name="msg">The message that describes the error.</param>
public class NotFound(string msg) : Exception($"{msg}")
{
}
