using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using ProjectManagement.Api.DataAccess;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Repository;
using ProjectManagement.Api.Services;
using ProjectManagement.Api.Services.Validator;

namespace ProjectManagement.Api.Tests.Repository
{
    public class ProjectRepositoryTest
    {
        [Test]
        public void Should_ThrowException_When_CreateProjectIsNotValid()
        {
            var mockProjectDao = Substitute.For<IProjectDao>();
            var mockProjectFactory = Substitute.For<IProjectFactory>();
            mockProjectFactory.CreateAsync(Arg.Any<ProjectDto>()).Returns(new Project
            {
                Owner = new Employee
                {
                    EmployeeRole = new EmployeeRole
                    {
                        Id = 1
                    }
                },
                Participants = new List<Employee>()
            });
            
            var projectValidator = new ProjectValidator();
            
            var projectRepository = new ProjectRepository(mockProjectDao, projectValidator, mockProjectFactory);

            Assert.ThrowsAsync<ArgumentException>(async () => await projectRepository.CreateProjectAsync(new ProjectDto()));
        }
        
        [Test]
        public void Should_ThrowException_When_UpdateProjectIsNotValid()
        {
            var mockProjectDao = Substitute.For<IProjectDao>();
            var mockProjectFactory = Substitute.For<IProjectFactory>();
            mockProjectFactory.CreateAsync(Arg.Any<ProjectDto>()).Returns(new Project
            {
                Owner = new Employee
                {
                    EmployeeRole = new EmployeeRole
                    {
                        Id = 1
                    }
                },
                Participants = new List<Employee>()
            });
            
            var projectValidator = new ProjectValidator();
            
            var projectRepository = new ProjectRepository(mockProjectDao, projectValidator, mockProjectFactory);

            Assert.ThrowsAsync<ArgumentException>(async () => await projectRepository.UpdateProjectAsync(new ProjectDto()));
        }
    }
}