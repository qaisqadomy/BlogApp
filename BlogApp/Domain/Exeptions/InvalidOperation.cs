namespace Domain.Exeptions;

/// <summary>
/// Represents an exception that is thrown when an invalid operation is attempted.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="InvalidOperation"/> class with a specified error message.
/// </remarks>
/// <param name="msg">The message that describes the error.</param>
public class InvalidOperation(string msg) : Exception(msg)
{
}
