using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ProjectManagement.Api.DataAccess;
using ProjectManagement.Api.Repository;
using ProjectManagement.Api.Services;

namespace ProjectManagement.Api.Tests.Repository
{
    public class EmployeeRepositoryTest
    {
        [Test]
        [TestCaseSource(typeof(EmployeeIsManagerTestCases))]
        public async Task Should_CheckEmployeeIsManager_When_EmployeeId(
            List<EmployeeDto> employees,
            bool exceptResult)
        {
            var mockEmployeeDao = Substitute.For<IEmployeeDao>();
            mockEmployeeDao.GetEmployeesAsync(Arg.Any<List<int>>()).Returns(employees);

            var mockEmployeeFactory = Substitute.For<IEmployeeFactory>();
            
            var employeeRepository = new EmployeeRepository(mockEmployeeDao, mockEmployeeFactory);

            Assert.AreEqual(exceptResult, await employeeRepository.IsEmployeeTeamManagerAsync(1));
        }

        private class EmployeeIsManagerTestCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new TestCaseData(
                    new List<EmployeeDto>(),
                    false
                ).SetName("Should_NotManager_When_NoEmployeeExist");
                
                yield return new TestCaseData(
                    new List<EmployeeDto>()
                    {
                        new EmployeeDto
                        {
                            EmployeeRoleId = 1
                        }
                    },
                    false
                ).SetName("Should_NotManager_When_EmployeeRoleIsNotManager");
                
                yield return new TestCaseData(
                    new List<EmployeeDto>()
                    {
                        new EmployeeDto
                        {
                            EmployeeRoleId = EmployeeRepository.TeamManager
                        }
                    },
                    true
                ).SetName("Should_Manager_When_EmployeeIsManager");
            }
        }
    }
}