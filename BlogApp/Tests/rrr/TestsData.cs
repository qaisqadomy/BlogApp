using Domain.Entities;

namespace RepositoriesTests;

public class TestsData
{

     public static readonly Article Article1 = new Article
        {
            Title = "Article 1",
            Tags = new List<string> { "Tech" },
            AuthorId = 1,
            Favorited = true,
            Slug = "qais",
            Description = "qais",
            Body = "dsds"
        };

        public static readonly Article Article2 = new Article
        {
            Title = "Article 2",
            Tags = new List<string> { "Science" },
            AuthorId = 1,
            Favorited = false,
            Slug = "qais",
            Description = "qais",
            Body = "dsds"
        };


        public static readonly User User = new User
        {
            UserName = "qais",
            Email = "qais@gmail.com",
            Password = "123"
        };

}
