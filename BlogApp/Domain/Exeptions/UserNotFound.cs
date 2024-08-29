using System;

namespace Domain.Exeptions;

public class UserNotFound(string msg) : NotFound(msg)
{
}
