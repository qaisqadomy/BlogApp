namespace Domain.Exeptions;

public class NotRegesterd(string msg) : InvalidOperation(msg)
{
}
