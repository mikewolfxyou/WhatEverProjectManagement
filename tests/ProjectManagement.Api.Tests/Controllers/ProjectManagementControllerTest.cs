using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using ProjectManagement.Api.Controllers;
using ProjectManagement.Api.DataAccess;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Repository;

namespace ProjectManagement.Api.Tests.Controllers
{
    public class ProjectManagementControllerTest
    {
        [Test]
        public async Task Should_BadRequest_When_ProjectRepoOperationFailed()
        {
            var mockRepo = Substitute.For<IProjectRepository>();
            mockRepo.CreateProjectAsync(Arg.Any<ProjectDto>()).Throws(new ArgumentException());
            
            var controller = new ProjectManagementController(mockRepo);

            var httpResult = await controller.Create(new ProjectDto());
            
            Assert.AreEqual(StatusCodes.Status400BadRequest, httpResult.StatusCode);
        }
        
        [Test]
        public async Task Should_BadRequest_When_ProjectRepoOperationThrowException()
        {
            var mockRepo = Substitute.For<IProjectRepository>();
            mockRepo.CreateProjectAsync(Arg.Any<ProjectDto>()).Throws(new Exception());
            
            var controller = new ProjectManagementController(mockRepo);

            var httpResult = await controller.Create(new ProjectDto());
            
            Assert.AreEqual(StatusCodes.Status400BadRequest, httpResult.StatusCode);
        }
        
        [Test]
        public async Task Should_Accept_When_ProjectRepoOperationSuccess()
        {
            var mockRepo = Substitute.For<IProjectRepository>();
            await mockRepo.CreateProjectAsync(Arg.Any<ProjectDto>());
            
            var controller = new ProjectManagementController(mockRepo);

            var httpResult = await controller.Create(new ProjectDto());
            
            Assert.AreEqual(StatusCodes.Status202Accepted, httpResult.StatusCode);
        }
        
        [Test]
        public async Task Should_BadRequest_When_ProjectRepoUpdateOperationFailed()
        {
            var mockRepo = Substitute.For<IProjectRepository>();
            mockRepo.UpdateProjectAsync(Arg.Any<ProjectDto>()).Throws(new ArgumentException());
            
            var controller = new ProjectManagementController(mockRepo);

            var httpResult = await controller.Update(new ProjectDto
            {
                Id = 0
            });
            
            Assert.AreEqual(StatusCodes.Status400BadRequest, httpResult.StatusCode);
        }
        
        [Test]
        public async Task Should_BadRequest_When_ProjectRepoUpdateOperationThrowException()
        {
            var mockRepo = Substitute.For<IProjectRepository>();
            mockRepo.UpdateProjectAsync(Arg.Any<ProjectDto>()).Throws(new Exception());
            
            var controller = new ProjectManagementController(mockRepo);

            var httpResult = await controller.Update(new ProjectDto
            {
                Id = 0
            });
            
            Assert.AreEqual(StatusCodes.Status400BadRequest, httpResult.StatusCode);
        }
        
        [Test]
        public async Task Should_Accept_When_ProjectRepoUpdateOperationSuccess()
        {
            var mockRepo = Substitute.For<IProjectRepository>();
            await mockRepo.UpdateProjectAsync(Arg.Any<ProjectDto>());
            
            var controller = new ProjectManagementController(mockRepo);

            var httpResult = await controller.Update(new ProjectDto
            {
                Id = 0
            });
            
            Assert.AreEqual(StatusCodes.Status202Accepted, httpResult.StatusCode);
        }
        
        [Test]
        public async Task Should_NotFound_When_ProjectNotFound()
        {
            var mockRepo = Substitute.For<IProjectRepository>();
            mockRepo.GetProjectAsync(Arg.Any<int>()).Returns(new NullProject());
            
            var controller = new ProjectManagementController(mockRepo);

            var httpResult = await controller.Update(new ProjectDto
            {
                Id = 0
            });
            
            Assert.AreEqual(StatusCodes.Status404NotFound, httpResult.StatusCode);
        }
    }
}