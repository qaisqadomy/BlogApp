namespace Domain.Exeptions;

public class InvalidOperation(string msg) : Exception($"{msg}");
