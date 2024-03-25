using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestResultsAnalysis.BLL;
using TestResultsAnalysis.BLL.Interfaces;
using TestResultsAnalysis.Models;

namespace TestResultsAnalysis.Utils
{
    public static class JsonManager
    {
        // Json Format Example string
        public static string JsonExample()
        {
            string jsonString = @"[
                    {""testCaseName"":""Log In"", ""passed"":true, ""executionTime"":4, ""timeStamp"":""2024-01-10T08:14:00""},
                    {""testCaseName"":""Log Out"", ""passed"":true, ""executionTime"":2, ""timeStamp"":""2024-01-10T08:15:00""},
                ]";

            var json = JsonConvert.DeserializeObject(jsonString);
            string formattedJson = JsonConvert.SerializeObject(json, Formatting.Indented);

            return formattedJson;
        }

        // Captures the Json inserted by the user
        public static string CaptureJson(bool createNewJson)
        {
            string finalJson;
            if (createNewJson)
            {
                Console.WriteLine("Json Capture will end after the user inputs the closing square bracket \"]\" symbol.");
                Console.WriteLine("Insert Json data");

                string json = BuildJson();
                finalJson = string.Empty;

                if (JsonIsValid(json))
                    finalJson = json;
                else
                {
                    Console.WriteLine("JSON string does not represent a valid list of TestCase objects. Please make sure the Json format matches the example");
                    CaptureJson(true);
                }
            }
            else
            {
                finalJson = JsonExample();
            }
            
            return finalJson;
        }

        // Builds the Json line by line until the user sends the closing square bracket char "]"
        public static string BuildJson()
        {
            string jsonLine;
            StringBuilder fullJson = new StringBuilder();

            while (true)
            {
                jsonLine = Console.ReadLine();
                fullJson.AppendLine(jsonLine);
                if (jsonLine.Contains("]"))
                {
                    break;
                }
            }
            return fullJson.ToString();
        }

        // Validates that the Json inserted by the user is a valid list of test cases
        private static bool JsonIsValid(string jsonString)
        {
            try
            {
                // Check if JSON string represents valid JSON syntax
                JToken.Parse(jsonString);

                // Check if JSON string can be deserialized into a list of TestCase objects
                List<TestCase> result = JsonConvert.DeserializeObject<List<TestCase>>(jsonString);

                if (result is List<TestCase> && result.Any(item => !IsObjectEmpty(item)))
                {
                    return true;
                }
            }
            catch (JsonException)
            {
                return false;
            }

            return false;
        }

        // Defines criteria for an empty TestCase object (handles en empty Json scenario)
        private static bool IsObjectEmpty(TestCase testCase)
        {
            return string.IsNullOrEmpty(testCase.TestCaseName) && testCase.Passed == false && testCase.ExecutionTime == 0 && testCase.TimeStamp == DateTime.MinValue.ToString();
        }

        // Calculates all the metrics
        public static void JsonTestCaseAnalysis(string jsonString, string fileName)
        {
            TestCaseRepository testCaseRepository = new TestCaseRepository();
            var finalJson = testCaseRepository.ConvertToJson(jsonString);
            var totalTestCases = testCaseRepository.GetTotalCasesCount(finalJson);
            var passedTestCases = testCaseRepository.GetPassedTestCasesCount(finalJson);
            var failedTestCases = testCaseRepository.GetFailedTestCasesCount(finalJson);
            var maxExecutionTime = testCaseRepository.GetMaxExecutionTime(finalJson);
            var minExecutionTime = testCaseRepository.GetMinExecutionTime(finalJson);
            DisplayMetrics(totalTestCases, passedTestCases, failedTestCases, maxExecutionTime, minExecutionTime);
            CSV.GenerateCSV(fileName, finalJson);
        }

        //Display the metrics to the user
        public static void DisplayMetrics(int totalTestCases, int passedTestCases, int failedTestCases, decimal maxExecutionTime, decimal minExecutionTime)
        {
            Console.WriteLine(string.Concat("\nTEST CASE RESULTS\n"));
            Console.WriteLine(string.Concat("Total Test Cases: ", totalTestCases));
            Console.WriteLine(string.Concat("Passed: ", passedTestCases));
            Console.WriteLine(string.Concat("Failed: ", failedTestCases));
            Console.WriteLine(string.Concat("Max Execution Time: ", maxExecutionTime));
            Console.WriteLine(string.Concat("Min Execution Time: ", minExecutionTime));
        }
    }
}
