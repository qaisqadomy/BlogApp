
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
            List<Comment> comments = new()
            {
                TestHelper.Comment1(),
                TestHelper.Comment2()
            };
            List<User> users = new()
            {
                TestHelper.User1(),
                TestHelper.User2()
            };

            mockCommentRepository.Setup(repo => repo.GetAll()).Returns(comments);
            mockUserRepository.Setup(repo => repo.GetByIds(It.IsAny<List<int>>())).Returns(users);

            List<CommentViewDTO> result = commentService.GetAll();
            
            Assert.Equal(2, result.Count);
            Assert.Equal(TestHelper.Comment1().Body, result[0].Body);
            Assert.Equal(TestHelper.Comment2().Body, result[1].Body);
            Assert.Equal( TestHelper.User1().UserName, result[0].UserDataDTO.UserName);
            Assert.Equal( TestHelper.User2().UserName, result[1].UserDataDTO.UserName);
        }

        [Fact]
        public void AddComment_CallsAddCommentOnRepository()
        {
            CommentDTO commentDto = TestHelper.CommentDto();
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
