using System.Collections;
using NUnit.Framework;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Services;

namespace ProjectManagement.Api.Tests.Services
{
    public class StateIsValidSpecificationTest
    {
        [Test]
        [TestCaseSource(typeof(StateIsValidTestCases))]
        public void Should_Satisfied_When_ProjectStateIsValid(
            Project project,
            bool exceptResult)
        {
            var stateIsValidSpecification = new StateIsValidSpecification();

            Assert.AreEqual(exceptResult, stateIsValidSpecification.IsSatisfiedBy(project));
        }

        private class StateIsValidTestCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                
                yield return new TestCaseData(
                    new Project
                    {
                        State = ProjectState.Active
                    },
                    true
                ).SetName("Should_Satisfied_When_ProjectStateIsActive");
                
                yield return new TestCaseData(
                    new Project
                    {
                        State = ProjectState.Planned
                    },
                    true
                ).SetName("Should_Satisfied_When_ProjectStateIsPlanned");
                
                yield return new TestCaseData(
                    new Project
                    {
                        State = ProjectState.Done
                    },
                    true
                ).SetName("Should_Satisfied_When_ProjectStateIsDone");
                
                yield return new TestCaseData(
                    new Project
                    {
                        State = ProjectState.Failed
                    },
                    true
                ).SetName("Should_Satisfied_When_ProjectStateIsFailed");
            }
        }
    }
}