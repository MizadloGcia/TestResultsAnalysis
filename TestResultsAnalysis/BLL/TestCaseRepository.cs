using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestResultsAnalysis.BLL.Interfaces;
using TestResultsAnalysis.Models;

namespace TestResultsAnalysis.BLL
{
    public class TestCaseRepository : ITestRepository
    {
        public List<TestCase> ConvertToJson(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<TestCase>>(jsonString);
        }

        public int GetFailedTestCasesCount(List<TestCase> testCasesList)
        {
            return testCasesList.Count(testCase => !testCase.Passed);
        }

        public decimal GetMaxExecutionTime(List<TestCase> testCasesList)
        {
            return testCasesList.Max(testCase => testCase.ExecutionTime);
        }

        public decimal GetMinExecutionTime(List<TestCase> testCasesList)
        {
            return testCasesList.Min(testCase => testCase.ExecutionTime);
        }

        public int GetPassedTestCasesCount(List<TestCase> testCasesList)
        {
            return testCasesList.Count(testCase => testCase.Passed);
        }

        public decimal GetTimeExecutionAverage(List<TestCase> testCasesList)
        {
            return testCasesList.Average(testCase => testCase.ExecutionTime);
        }

        public int GetTotalCasesCount(List<TestCase> testCasesList)
        {
            return testCasesList.Count;
        }
    }
}
