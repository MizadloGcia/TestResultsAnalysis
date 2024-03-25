using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestResultsAnalysis.Models;

namespace TestResultsAnalysis.Utils
{
    public static class CSV
    {
        private static readonly string folderName = "Test-Results-Analysis";
        private static readonly string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);

        // Creates the CVS directory
        private static bool CreateFolder()
        {
            try
            {
                Directory.CreateDirectory(path);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while creating folder: {ex.Message}");
                return false;
            }
        }

        // Creates the CSV File
        public static void GenerateCSV(string fileName, List<TestCase> testCases)
        {
            if (CreateFolder())
            {
                string fullpath = Path.Combine(path, fileName);
                File.Create(fullpath).Close();
                ExportJSONToCsv(fullpath, testCases);
                Console.WriteLine($"\nCreated CSV file in the following path: {fullpath}");
                Console.WriteLine("Press any key to return to the main menu");
                Console.ReadKey();
            }
        }

        // Validates that there is no file in the Directory with the same name
        public static bool ValidateFileNameNotExist(string fileName)
        {
            Directory.CreateDirectory(path);
            string[] Files = Directory.GetFiles(path);
            for (int i = 0; i < Files.Length; i++)
            {
                if (string.Equals(fileName, Path.GetFileName(Files[i]), StringComparison.Ordinal))
                    return false;
            }
            return true;
        }

        // Writes the CSV data into the CSV file
        public static void ExportJSONToCsv(string csvFilePath, List<TestCase> testCases)
        {
            // Input validation
            if (csvFilePath is null)
            {
                throw new ArgumentNullException(nameof(csvFilePath), "CSV file path cannot be null.");
            }

            if (testCases is null)
            {
                throw new ArgumentNullException(nameof(testCases), "Test cases list cannot be null.");
            }

            try
            {
                using (var fileWriter = new StreamWriter(csvFilePath))
                {
                    fileWriter.WriteLine("TestCaseName,Passed,ExecutionTime,TimeStamp");

                    foreach (var testCase in testCases)
                    {
                        fileWriter.WriteLine($"{EscapeCsvField(testCase.TestCaseName)},{testCase.Passed},{testCase.ExecutionTime},{testCase.TimeStamp:yyyy-MM-dd HH:mm:ss}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error exporting Json to CSV: " + ex.Message);
            }

        }

        // Function to escape CSV fields (,)
        private static string EscapeCsvField(string field)
        {
            // If the field contains a comma, enclose it in double quotes and escape any existing double quotes
            if(field != null)
            {
                if (field.Contains(","))
                {
                    return $"\"{field.Replace("\"", "\"\"")}\"";
                }
            }
            return field;
        }

        // Appends ".csv" extension into the file if not added by the user
        public static string CheckCsvExtension(string fileName)
        {
            const string csvExtension = ".csv";

            // Check if the file name already has a CSV extension
            if (!fileName.EndsWith(csvExtension, StringComparison.OrdinalIgnoreCase))
            {
                // Append CSV extension if missing
                fileName = $"{fileName.TrimEnd('.')}{csvExtension}";
            }

            return fileName;
        }
    }
}
