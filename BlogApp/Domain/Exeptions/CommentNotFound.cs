using System;

namespace Domain.Exeptions;

public class CommentNotFound(string msg) : NotFound(msg)
{
}
