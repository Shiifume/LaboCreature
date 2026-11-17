using LaboCreature.BLL.Exceptions;
using LaboCreature.BLL.Helpers;
using LaboCreature.BLL.Models;
using LaboCreature.BLL.Services;
using LaboCreature.DAL.Entities;
using LaboCreature.DAL.Interfaces;
using Moq;

namespace LaboCreature.Test.Services
{
    public class AuthServiceTest
    {
        [Fact]
        public void CreateMember_ValidMember_ReturnsId()
        {
            //Arrange
            //Mocks
            var mockRepo = new Mock<IMemberRepository>();
            var mockHasher = new Mock<IPasswordHasher>();

            //Setups
            mockRepo.Setup(r => r.GetByEmail("test@test.com"))
                    .Returns((MemberEntity?)null);

            mockRepo.Setup(r => r.GetByUsername("username"))
                    .Returns((MemberEntity?)null);

            mockHasher.Setup(h => h.HashPassword("password"))
                      .Returns("hashedPassword");

            mockRepo.Setup(r => r.CreateMember(It.IsAny<MemberEntity>()))
                    .Returns(42);

            var service = new AuthService(mockHasher.Object, mockRepo.Object);

            var memberToRegister = new RegisterMemberModel()
            {
                Email = "test@test.com",
                Username = "username",
                Password = "password"
            };

            //Act
            var result = service.CreateMember(memberToRegister);

            //Assess
            Assert.Equal(42, result);

            mockHasher.Verify(h => h.HashPassword("password"), Times.Once);

            mockRepo.Verify(r => r.CreateMember(
                It.Is<MemberEntity>(m =>
                    m.Email == "test@test.com" &&
                    m.Username == "username" &&
                    m.PasswordHash == "hashedPassword"
                )));
        }

        [Fact]
        public void CreateMember_UsedUsername_ThrowsException()
        {
            //Arrange

            var mockRepo = new Mock<IMemberRepository>();
            var mockHasher = new Mock<IPasswordHasher>();

            mockRepo.Setup(m => m.GetByEmail("test@test.com"))
                    .Returns((MemberEntity?)null);

            mockRepo.Setup(m => m.GetByUsername("username"))
                    .Returns(new MemberEntity()
                    {
                        Id = 1,
                        Email = "a@a.com",
                        Username = "username",
                        PasswordHash = "passwordHashed"
                    });

            var service = new AuthService(mockHasher.Object, mockRepo.Object);

            var memberToRegister = new RegisterMemberModel()
            {
                Email = "test@test.com",
                Username = "username",
                Password = "password"
            };

            //act
            var ex = Assert.Throws<MemberUsernameAlreadyExists>(
                () => service.CreateMember(memberToRegister)
                );

            //assess
            Assert.Equal("Username taken", ex.Message);
            mockHasher.Verify(h => h.HashPassword("password"), Times.Never);
            mockRepo.Verify(r => r.CreateMember(It.IsAny<MemberEntity>()), Times.Never);
        }

        [Fact]
        public void CreateMember_UsedEmail_ThrowsException()
        {
            //Arrange

            var mockRepo = new Mock<IMemberRepository>();
            var mockHasher = new Mock<IPasswordHasher>();

            mockRepo.Setup(m => m.GetByEmail("test@test.com"))
                    .Returns(new MemberEntity()
                    {
                        Id = 1,
                        Email = "a@a.com",
                        Username = "username",
                        PasswordHash = "passwordHashed"
                    });
                    
            var service = new AuthService(mockHasher.Object, mockRepo.Object);

            var memberToRegister = new RegisterMemberModel()
            {
                Email = "test@test.com",
                Username = "username",
                Password = "password"
            };

            //act
            var ex = Assert.Throws<MemberEmailAlreadyExists>(
                () => service.CreateMember(memberToRegister)
                );

            //assess
            Assert.Equal("An account already exists with this email.", ex.Message);
            mockHasher.Verify(h => h.HashPassword("password"), Times.Never);
            mockRepo.Verify(r => r.CreateMember(It.IsAny<MemberEntity>()), Times.Never);
            mockRepo.Verify(r => r.GetByUsername(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Login_UnknownEmail_ReturnsNull()
        {
            // ARRANGE

            var mockRepo = new Mock<IMemberRepository>();
            var mockHasher = new Mock<IPasswordHasher>();

            mockRepo.Setup(r => r.GetByEmail("unknown@test.com"))
                    .Returns((MemberEntity?)null);

            var service = new AuthService(
                mockHasher.Object,
                mockRepo.Object
            );

            var loginModel = new LoginMemberModel()
            {
                Email = "unknown@test.com",
                Password = "password"
            };

            // ACT

            var result = service.Login(loginModel);

            // ASSERT

            Assert.Null(result);

            mockHasher.Verify(
                h => h.Verify(It.IsAny<string>(), It.IsAny<string>()),
                Times.Never
            );
        }

        [Fact]
        public void Login_IncorrectPassword_ReturnsNull()
        {
            // ARRANGE

            var mockRepo = new Mock<IMemberRepository>();
            var mockHasher = new Mock<IPasswordHasher>();

            mockRepo.Setup(r => r.GetByEmail("test@test.com"))
                    .Returns(new MemberEntity()
                    {
                        Id = 1,
                        Email = "test@test.com",
                        Username = "testuser",
                        PasswordHash = "hashedPassword"
                    });

            mockHasher.Setup(h => h.Verify("wrongPassword", "hashedPassword"))
                      .Returns(false);

            var service = new AuthService(
                mockHasher.Object,
                mockRepo.Object
            );

            var loginModel = new LoginMemberModel()
            {
                Email = "test@test.com",
                Password = "wrongPassword"
            };

            // ACT

            var result = service.Login(loginModel);

            // ASSERT

            Assert.Null(result);

            mockHasher.Verify(
                h => h.Verify("wrongPassword", "hashedPassword"),
                Times.Once
            );
        }

        [Fact]
        public void Login_ValidCredentials_ReturnsMember()
        {
            // ARRANGE

            var mockRepo = new Mock<IMemberRepository>();
            var mockHasher = new Mock<IPasswordHasher>();

            mockRepo.Setup(r => r.GetByEmail("test@test.com"))
                    .Returns(new MemberEntity()
                    {
                        Id = 1,
                        Email = "test@test.com",
                        Username = "testuser",
                        PasswordHash = "hashed_password"
                    });

            mockHasher.Setup(h => h.Verify("password", "hashed_password"))
                      .Returns(true);

            var service = new AuthService(
                mockHasher.Object,
                mockRepo.Object
            );

            var loginModel = new LoginMemberModel()
            {
                Email = "test@test.com",
                Password = "password"
            };

            // ACT

            var result = service.Login(loginModel);

            // ASSERT

            Assert.NotNull(result);

            Assert.Equal(1, result.Id);
            Assert.Equal("test@test.com", result.Email);
            Assert.Equal("testuser", result.Username);

            mockHasher.Verify(
                h => h.Verify("password", "hashed_password"),
                Times.Once
            );
        }
    }
}
