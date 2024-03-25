using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestResultsAnalysis.Utils
{
    public class Menu
    {
        string FileName = string.Empty;
        public void DisplayOptions()
        {
            Console.WriteLine("Json FORMAT EXAMPLE:\n");
            Console.WriteLine(JsonManager.JsonExample());
            Console.WriteLine("\nSelect an Option:");
            Console.WriteLine("0. Insert new Json (Test Case data)");
            Console.WriteLine("1. Convert Json to CSV using the data from the example above");
            Console.WriteLine("2. Exit");
            TriggerSelectedOption();
        }

        private void TriggerSelectedOption()
        {
            string selectedOption = Console.ReadLine();

            if(int.TryParse(selectedOption, out int value))
            {
                switch (value)
                {
                    case 0:
                        FileNameOptions(true);
                        break;
                    case 1:
                        FileNameOptions(false);
                        break;
                    case 2:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Option not valid... Press any key to return to the main menu");
                        Console.ReadKey();
                        break;
                }
                DisplayOptions();
            }
        }

        private void FileNameOptions(bool createNewJson)
        {
            BuildStandardFileName();
            Console.WriteLine($"Filename will be \"{FileName}\"");
            Console.WriteLine("0. Continue");
            Console.WriteLine("1. Change File Name");

            string selectedOption = Console.ReadLine();

            if (int.TryParse(selectedOption, out int value))
            {
                switch (value)
                {
                    case 0:
                        JsonManager.JsonTestCaseAnalysis(JsonManager.CaptureJson(createNewJson), FileName);
                        break;
                    case 1:
                        BuildNewFileName();
                        JsonManager.JsonTestCaseAnalysis(JsonManager.CaptureJson(createNewJson), FileName);
                        break;
                    default:
                        Console.WriteLine("Option not valid");
                        break;
                }
            }
        }

        private void BuildStandardFileName()
        {
            FileName = $"JsonToCSV-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.csv";
        }

        private void BuildNewFileName()
        {
            Console.WriteLine("Insert File Name");
            string fileName = CSV.CheckCsvExtension(Console.ReadLine());

            // Validates that there is no file in the Directory with the same name
            if (!CSV.ValidateFileNameNotExist(fileName))
            {
                Console.WriteLine("This location already has a file with the same name.");
                Console.ReadKey();
                DisplayOptions();
            }

            // Validates that the file name inserted by the user does not contains invalid chars ( \ / : * ? " < > |)
            if (fileName.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) != -1)
            {
                Console.WriteLine("File name is invalid");
                BuildNewFileName();
            }
            else
            {
                FileName = fileName;
            }
        }
    }
}
