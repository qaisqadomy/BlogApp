
using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.IRepositories;
using Moq;

namespace ServicesTests;

public class CommentServiceTests
{
     private readonly Mock<ICommentRepository> mockCommentRepository;
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly CommentService commentService;

        public CommentServiceTests()
        {
            mockCommentRepository = new Mock<ICommentRepository>();
            mockUserRepository = new Mock<IUserRepository>();
            commentService = new CommentService(mockCommentRepository.Object, mockUserRepository.Object);
        }

        [Fact]
        public void GetAll_ReturnsCommentViewDTOs()
        {
            
            List<Comment> comments = new List<Comment>
            {
                new() { Body = "Comment 1", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, AuthorId = 1 },
                new() { Body = "Comment 2", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, AuthorId = 2 }
            };
            List<User> users = new List<User>
            {
                new() { Id = 1, UserName = "User1", Email = "user1@example.com",Password="123",Bio = "Bio1", Image = "Image1", Following = true },
                new() { Id = 2, UserName = "User2", Email = "user2@example.com",Password="123", Bio = "Bio2", Image = "Image2", Following = false }
            };

            mockCommentRepository.Setup(repo => repo.GetAll()).Returns(comments);
            mockUserRepository.Setup(repo => repo.GetByIds(It.IsAny<List<int>>())).Returns(users);

            List<CommentViewDTO> result = commentService.GetAll();
            
            Assert.Equal(2, result.Count);
            Assert.Equal("Comment 1", result[0].Body);
            Assert.Equal("Comment 2", result[1].Body);
            Assert.Equal("User1", result[0].UserDataDTO.UserName);
            Assert.Equal("User2", result[1].UserDataDTO.UserName);
        }

        [Fact]
        public void AddComment_CallsAddCommentOnRepository()
        {
            
            CommentDTO commentDto = new CommentDTO
            {
                Body = "New Comment",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                AuthorId = 1
            };
            commentService.AddComment(commentDto);
            mockCommentRepository.Verify(repo => repo.AddComment(It.IsAny<Comment>()), Times.Once);
        }

        [Fact]
        public void DeleteComment_CallsDeleteCommentOnRepository()
        {
            
            int commentId = 1;

            
            commentService.DeleteComment(commentId);

            
            mockCommentRepository.Verify(repo => repo.DeleteComment(commentId), Times.Once);
        }
    }
