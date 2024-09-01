namespace Domain.Exeptions;

public class InvalidToken(string msg) : InvalidOperation(msg)
{

}
