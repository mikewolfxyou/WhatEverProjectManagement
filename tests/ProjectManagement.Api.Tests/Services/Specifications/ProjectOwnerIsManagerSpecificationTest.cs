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
            var projectOwnerIsManagerSpecification = new ProjectOwnerIsManagerSpecification();
            
            Assert.AreEqual(false, projectOwnerIsManagerSpecification.IsSatisfiedBy(
                new Project
                {
                    Owner = new Employee
                    {
                        EmployeeRole = new EmployeeRole
                        {
                            Id = 1
                        }
                    }
                }));
        }
        
        [Test]
        public void Should_Satisfied_When_ProjectOwnerIsManager()
        {
            var projectOwnerIsManagerSpecification = new ProjectOwnerIsManagerSpecification();
            Assert.AreEqual(true, projectOwnerIsManagerSpecification.IsSatisfiedBy(
                new Project
                {
                    Owner = new Employee
                    {
                        EmployeeRole = new EmployeeRole
                        {
                            Id = EmployeeRepository.TeamManager
                        }
                    }
                }));
        }
    }
}