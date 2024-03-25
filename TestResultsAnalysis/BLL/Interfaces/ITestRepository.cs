using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestResultsAnalysis.Models;

namespace TestResultsAnalysis.BLL.Interfaces
{
    internal interface ITestRepository
    {
        int GetTotalCasesCount(List<TestCase> testCasesList);
        int GetPassedTestCasesCount(List<TestCase> testCasesList);
        int GetFailedTestCasesCount(List<TestCase> testCasesList);
        decimal GetTimeExecutionAverage(List<TestCase> testCasesList);
        decimal GetMinExecutionTime(List<TestCase> testCasesList);
        decimal GetMaxExecutionTime(List<TestCase> testCasesList);
        List<TestCase> ConvertToJson(string jsonString);
    }
}
