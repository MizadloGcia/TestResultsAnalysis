using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TestResultsAnalysis.Models;
using TestResultsAnalysis.BLL;

namespace TestResultsAnalysisUnitTests
{
    [TestClass]
    public class TestCaseTests
    {
        [TestMethod]
        public void GetFailedTestCasesCount_ReturnsCorrectCount()
        {
            // Arrange
            TestCaseRepository testCaseRepository = new TestCaseRepository();
            var testCasesList = new List<TestCase>
            
        {
            new TestCase { Passed = true },
            new TestCase { Passed = false },
            new TestCase { Passed = false }
        };

            // Act
            var result = testCaseRepository.GetFailedTestCasesCount(testCasesList);

            // Assert
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GetMaxExecutionTime_ReturnsMaxExecutionTime()
        {
            // Arrange
            TestCaseRepository testCaseRepository = new TestCaseRepository();
            var testCasesList = new List<TestCase>
        {
            new TestCase { ExecutionTime = 10 },
            new TestCase { ExecutionTime = 20 },
            new TestCase { ExecutionTime = 15 }
        };

            // Act
            var result = testCaseRepository.GetMaxExecutionTime(testCasesList);

            // Assert
            Assert.AreEqual(20, result);
        }

        [TestMethod]
        public void GetMinExecutionTime_ReturnsMinExecutionTime()
        {
            // Arrange
            TestCaseRepository testCaseRepository = new TestCaseRepository();
            var testCasesList = new List<TestCase>
        {
            new TestCase { ExecutionTime = 10 },
            new TestCase { ExecutionTime = 20 },
            new TestCase { ExecutionTime = 15 }
        };

            // Act
            var result = testCaseRepository.GetMinExecutionTime(testCasesList);

            // Assert
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void GetPassedTestCasesCount_ReturnsCorrectCount()
        {
            // Arrange
            TestCaseRepository testCaseRepository = new TestCaseRepository();
            var testCasesList = new List<TestCase>
        {
            new TestCase { Passed = true },
            new TestCase { Passed = false },
            new TestCase { Passed = true }
        };

            // Act
            var result = testCaseRepository.GetPassedTestCasesCount(testCasesList);

            // Assert
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GetTimeExecutionAverage_ReturnsCorrectAverage()
        {
            // Arrange
            TestCaseRepository testCaseRepository = new TestCaseRepository();
            var testCasesList = new List<TestCase>
        {
            new TestCase { ExecutionTime = 10 },
            new TestCase { ExecutionTime = 20 },
            new TestCase { ExecutionTime = 15 }
        };

            // Act
            var result = testCaseRepository.GetTimeExecutionAverage(testCasesList);

            // Assert
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void GetTotalCasesCount_ReturnsCorrectCount()
        {
            // Arrange
            TestCaseRepository testCaseRepository = new TestCaseRepository();
            var testCasesList = new List<TestCase>
        {
            new TestCase(),
            new TestCase(),
            new TestCase()
        };

            // Act
            var result = testCaseRepository.GetTotalCasesCount(testCasesList);

            // Assert
            Assert.AreEqual(3, result);
        }
    }
}
