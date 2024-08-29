using System;

namespace Domain.Exeptions;

public class ArticleNotFound(string msg) : NotFound(msg)
{
}
