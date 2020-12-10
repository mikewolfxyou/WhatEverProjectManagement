using System;
using NSubstitute;
using NUnit.Framework;
using ProjectManagement.Api.DataAccess;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Repository;
using ProjectManagement.Api.Services.Validator;

namespace ProjectManagement.Api.Tests.Repository
{
    public class ProjectRepositoryTest
    {
        [Test]
        public void Should_ThrowException_When_CreateProjectIsNotValid()
        {
            var mockEmployeeRepository = Substitute.For<IEmployeeRepository>();
            mockEmployeeRepository.IsEmployeeTeamManagerAsync(Arg.Any<int>()).Returns(false);

            var mockProjectDao = Substitute.For<IProjectDao>();
            
            var projectValidator = new ProjectValidator(mockEmployeeRepository);
            
            var projectRepository = new ProjectRepository(mockProjectDao, projectValidator);

            Assert.ThrowsAsync<ArgumentException>(async () => await projectRepository.CreateProjectAsync(new Project()));
        }
        
        [Test]
        public void Should_ThrowException_When_UpdateProjectIsNotValid()
        {
            var mockEmployeeRepository = Substitute.For<IEmployeeRepository>();
            mockEmployeeRepository.IsEmployeeTeamManagerAsync(Arg.Any<int>()).Returns(false);

            var mockProjectDao = Substitute.For<IProjectDao>();
            
            var projectValidator = new ProjectValidator(mockEmployeeRepository);
            
            var projectRepository = new ProjectRepository(mockProjectDao, projectValidator);

            Assert.ThrowsAsync<ArgumentException>(async () => await projectRepository.UpdateProjectAsync(new Project()));
        }
    }
}