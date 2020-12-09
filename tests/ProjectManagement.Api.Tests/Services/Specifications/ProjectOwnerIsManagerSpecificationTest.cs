using NSubstitute;
using NUnit.Framework;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Repository;
using ProjectManagement.Api.Services.Validator.Specifications;

namespace ProjectManagement.Api.Tests.Services.Specifications
{
    public class ProjectOwnerIsManagerSpecificationTest
    {
        [Test]
        public void Should_NotSatisfied_When_ProjectOwnerIsNotManager()
        {
            var mockEmployeeRepo = Substitute.For<IEmployeeRepository>();
            mockEmployeeRepo.IsEmployeeTeamManagerAsync(Arg.Any<int>()).Returns(false);
            var projectOwnerIsManagerSpecification = new ProjectOwnerIsManagerSpecification(mockEmployeeRepo);
            
            Assert.AreEqual(false, projectOwnerIsManagerSpecification.IsSatisfiedBy(
                new Project()));
        }
        
        [Test]
        public void Should_Satisfied_When_ProjectOwnerIsManager()
        {
            var mockEmployeeRepo = Substitute.For<IEmployeeRepository>();
            mockEmployeeRepo.IsEmployeeTeamManagerAsync(Arg.Any<int>()).Returns(true);
            var projectOwnerIsManagerSpecification = new ProjectOwnerIsManagerSpecification(mockEmployeeRepo);
            
            Assert.AreEqual(true, projectOwnerIsManagerSpecification.IsSatisfiedBy(
                new Project()));
        }
    }
}