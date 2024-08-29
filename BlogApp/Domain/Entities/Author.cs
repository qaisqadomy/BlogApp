using System;

namespace Domain.Entities;

public class Author : User
{
     
    public string? Bio { get; set; }
    public string? Image { get; set; }
    public bool Following { get; set; }

}
