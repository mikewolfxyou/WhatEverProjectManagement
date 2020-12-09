using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using ProjectManagement.Api.DataAccess;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Repository;

namespace ProjectManagement.Api.Tests.Repository
{
    public class EmployeeRepositoryTest
    {
        [Test]
        [TestCaseSource(typeof(EmployeeIsManagerTestCases))]
        public void Should_CheckEmployeeIsManager_When_EmployeeId(
            List<Employee> employees,
            bool exceptResult)
        {
            var mockEmployeeDao = Substitute.For<IEmployeeDao>();
            mockEmployeeDao.GetEmployeesAsync(Arg.Any<List<int>>()).Returns(employees);
            
            var employeeRepository = new EmployeeRepository(mockEmployeeDao);

            Assert.AreEqual(exceptResult, employeeRepository.IsEmployeeTeamManagerAsync(1));
        }

        private class EmployeeIsManagerTestCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new TestCaseData(
                    new List<Employee>(),
                    false
                ).SetName("Should_NotManager_When_NoEmployeeExist");
                
                yield return new TestCaseData(
                    new List<Employee>
                    {
                        new NullEmployee()
                    },
                    false
                ).SetName("Should_NotManager_When_EmployeeIsNullEmployee");
                
                yield return new TestCaseData(
                    new List<Employee>()
                    {
                        new Employee
                        {
                            EmployeeRole = EmployeeRole.SoftwareEngineer
                        }
                    },
                    false
                ).SetName("Should_NotManager_When_EmployeeRoleIsNotManager");
                
                yield return new TestCaseData(
                    new List<Employee>()
                    {
                        new Employee
                        {
                            EmployeeRole = EmployeeRole.TeamManager
                        }
                    },
                    true
                ).SetName("Should_Manager_When_EmployeeIsManager");
            }
        }
    }
}