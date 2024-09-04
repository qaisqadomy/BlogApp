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

        Comment comment1 = TestHelper.Comment1();
        Comment comment2 = TestHelper.Comment2();
        _commentRepository.AddComment(comment1);
        _commentRepository.AddComment(comment2);

        List<Comment> result = _commentRepository.GetAll();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(result, c => c.Body == TestHelper.Comment1().Body);
        Assert.Contains(result, c => c.Body == TestHelper.Comment2().Body);
    }

    [Fact]
    public void AddComment_ShouldAddCommentToDatabase()
    {

        Comment comment = TestHelper.Comment1();

        _commentRepository.AddComment(comment);
        List<Comment> result = _commentRepository.GetAll();

        Assert.Single(result);
        Assert.Equal(TestHelper.Comment1().Body, result.First().Body);
    }

    [Fact]
    public void DeleteComment_ShouldRemoveCommentFromDatabase()
    {

        Comment comment = TestHelper.Comment1();
        _commentRepository.AddComment(comment);

        _commentRepository.DeleteComment(1);
        List<Comment> result = _commentRepository.GetAll();

        Assert.Empty(result);
    }

    [Fact]
    public void DeleteComment_ShouldThrowExceptionWhenCommentNotFound()
    {

        Exception exception = Assert.Throws<CommentNotFound>(() => _commentRepository.DeleteComment(999));
        Assert.Equal("Comment with the Id : 999 not found", exception.Message);
    }
}

