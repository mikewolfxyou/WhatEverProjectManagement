using System.Collections;
using NUnit.Framework;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Services.Validator.Specifications;

namespace ProjectManagement.Api.Tests.Services.Specifications
{
    public class ProgressIsValidSpecificationTest
    {
        [Test]
        [TestCaseSource(typeof(ProgressIsValidTestCases))]
        public void Should_Satisfied_When_ProjectProgressIsValid(
            Project project,
            bool exceptResult)
        {
            var progressIsValidSpecification = new ProgressIsValidSpecification();

            Assert.AreEqual(exceptResult, progressIsValidSpecification.IsSatisfiedBy(project));
        }

        private class ProgressIsValidTestCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new TestCaseData(
                    new Project
                    {
                        Progress = -1
                    },
                    false
                ).SetName("Should_NotSatisfied_When_ProjectProgressIsMinus");

                yield return new TestCaseData(
                    new Project
                    {
                        Progress = 100.01f
                    },
                    false
                ).SetName("Should_NotSatisfied_When_ProjectProgressIsMoreThan100");

                yield return new TestCaseData(
                    new Project
                    {
                        Progress = 99.99f
                    },
                    true
                ).SetName("Should_Satisfied_When_ProjectProgressBetween0And100");
            }
        }
    }
}