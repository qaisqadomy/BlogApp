namespace Domain.Exeptions;

/// <summary>
/// Represents an exception that is thrown when a requested entity is not found.
/// </summary>
public class NotFound(string msg) : Exception($"{msg}")
{
}
