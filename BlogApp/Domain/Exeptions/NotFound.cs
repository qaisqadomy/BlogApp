namespace Domain.Exeptions;

public class NotFound(string msg) : Exception($"{msg}");
