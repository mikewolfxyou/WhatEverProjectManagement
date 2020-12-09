using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Repository;
using ProjectManagement.Api.Services.Validator.Specifications;

namespace ProjectManagement.Api.Tests.Services
{
    public class ParticipantInSameDepartmentOfOwnerSpecificationTest
    {
        [Test]
        public void Should_NotSatisfied_When_OneParticipantNotSameDepartmentWithProjectOwner()
        {
            var mockEmployeeRepo = Substitute.For<IEmployeeRepository>();
            mockEmployeeRepo.GetEmployeeAsync(Arg.Any<int>()).Returns(new Employee
            {
                DepartmentId = 1
            });
            
            mockEmployeeRepo.GetEmployeesAsync(Arg.Any<List<int>>()).Returns(
                new List<Employee>
                {
                    new Employee
                    {
                        DepartmentId = 2
                    },
                    new Employee
                    {
                        DepartmentId = 1
                    },
                });

            var participantInSameDepartmentOfOwnerSpecification =
                new ParticipantInSameDepartmentOfOwnerSpecification(mockEmployeeRepo);

            Assert.AreEqual(false, participantInSameDepartmentOfOwnerSpecification.IsSatisfiedBy(new Project()));
        }

        [Test]
        public void Should_Satisfied_When_AllParticipantSameDepartmentWithProjectOwner()
        {
            var mockEmployeeRepo = Substitute.For<IEmployeeRepository>();
            mockEmployeeRepo.GetEmployeeAsync(Arg.Any<int>()).Returns(new Employee
            {
                DepartmentId = 1
            });
            
            mockEmployeeRepo.GetEmployeesAsync(Arg.Any<List<int>>()).Returns(
                new List<Employee>
                {
                    new Employee
                    {
                        DepartmentId = 1
                    },
                    new Employee
                    {
                        DepartmentId = 1
                    },
                });

            var participantInSameDepartmentOfOwnerSpecification =
                new ParticipantInSameDepartmentOfOwnerSpecification(mockEmployeeRepo);

            Assert.AreEqual(true, participantInSameDepartmentOfOwnerSpecification.IsSatisfiedBy(new Project()));
        }
    }
}