namespace Domain.Exeptions;

/// <summary>
/// Represents an exception that is thrown when an operation is attempted with a user who is not registered.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="NotRegesterd"/> class with a specified error message.
/// </remarks>
/// <param name="msg">The message that describes the error.</param>
public class NotRegesterd(string msg) : InvalidOperation(msg)
{
}
