using Domain.Entities;
using Domain.Exeptions;
using Application.Repositories;

namespace RepositoriesTests;

public class CommentRepositoryTests
{
    private readonly InMemoryDB _inMemoryDb;
    private readonly CommentRepository _commentRepository;

    public CommentRepositoryTests()
    {
        _inMemoryDb = new InMemoryDB();
        _commentRepository = new CommentRepository(_inMemoryDb.DbContext);
    }

    [Fact]
    public void GetAll_ShouldReturnAllComments()
    {

        var comment1 = new Comment { Id = 1, Body = "First comment", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, AuthorId = 1 };
        var comment2 = new Comment { Id = 2, Body = "Second comment", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, AuthorId = 1 };
        _commentRepository.AddComment(comment1);
        _commentRepository.AddComment(comment2);


        var result = _commentRepository.GetAll();


        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(result, c => c.Body == "First comment");
        Assert.Contains(result, c => c.Body == "Second comment");
    }

    [Fact]
    public void AddComment_ShouldAddCommentToDatabase()
    {

        var comment = new Comment { Id = 1, Body = "New comment", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, AuthorId = 1 };


        _commentRepository.AddComment(comment);
        var result = _commentRepository.GetAll();


        Assert.Single(result);
        Assert.Equal("New comment", result.First().Body);
    }

    [Fact]
    public void DeleteComment_ShouldRemoveCommentFromDatabase()
    {

        var comment = new Comment { Id = 1, Body = "Comment to delete", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, AuthorId = 1 };
        _commentRepository.AddComment(comment);


        _commentRepository.DeleteComment(1);
        var result = _commentRepository.GetAll();


        Assert.Empty(result);
    }

    [Fact]
    public void DeleteComment_ShouldThrowExceptionWhenCommentNotFound()
    {

        var exception = Assert.Throws<CommentNotFound>(() => _commentRepository.DeleteComment(999));
        Assert.Equal("Comment with the Id : 999 Not Found", exception.Message);
    }

    public void Dispose()
    {
        _inMemoryDb.Dispose();
    }
}

