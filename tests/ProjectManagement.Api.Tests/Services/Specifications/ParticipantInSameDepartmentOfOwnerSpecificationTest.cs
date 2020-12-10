using System.Collections.Generic;
using NUnit.Framework;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Services.Validator.Specifications;

namespace ProjectManagement.Api.Tests.Services.Specifications
{
    public class ParticipantInSameDepartmentOfOwnerSpecificationTest
    {
        [Test]
        public void Should_NotSatisfied_When_OneParticipantNotSameDepartmentWithProjectOwner()
        {
            var participantInSameDepartmentOfOwnerSpecification =
                new ParticipantInSameDepartmentOfOwnerSpecification();

            Assert.AreEqual(false, participantInSameDepartmentOfOwnerSpecification.IsSatisfiedBy(
                new Project
                {
                    Owner = new Employee
                    {
                        Department = new Department {Id = 1}
                    },
                    Participants = new List<Employee>
                    {
                        new Employee
                        {
                            Department = new Department {Id = 2}
                        },
                        new Employee
                        {
                            Department = new Department {Id = 1}
                        },
                    }
                }));
        }

        [Test]
        public void Should_Satisfied_When_AllParticipantSameDepartmentWithProjectOwner()
        {
            var participantInSameDepartmentOfOwnerSpecification =
                new ParticipantInSameDepartmentOfOwnerSpecification();

            Assert.AreEqual(true, participantInSameDepartmentOfOwnerSpecification.IsSatisfiedBy(
                new Project
                {
                    Owner = new Employee
                    {
                        Department = new Department {Id = 1}
                    },
                    Participants = new List<Employee>
                    {
                        new Employee
                        {
                            Department = new Department {Id = 1}
                        },
                        new Employee
                        {
                            Department = new Department {Id = 1}
                        },
                    }
                }));
        }
    }
}