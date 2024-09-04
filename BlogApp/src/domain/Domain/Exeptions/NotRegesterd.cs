namespace Domain.Exeptions;

/// <summary>
/// Represents an exception that is thrown when an operation is attempted with a user who is not registered.
/// </summary>
public class NotRegesterd(string msg) : InvalidOperation(msg)
{
}
