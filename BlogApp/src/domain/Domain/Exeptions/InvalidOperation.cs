namespace Domain.Exeptions;

/// <summary>
/// Represents an exception that is thrown when an invalid operation is attempted.
/// </summary>
public class InvalidOperation(string msg) : Exception(msg)
{
}
