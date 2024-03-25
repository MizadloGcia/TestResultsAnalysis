using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestResultsAnalysis.Models
{
    public sealed class TestCase
    {
        [JsonProperty("testCaseName")]
        public string TestCaseName { get; set; }
        [JsonProperty("passed")]
        public bool Passed { get; set; }
        [JsonProperty("executionTime")]
        public decimal ExecutionTime { get; set; }
        [JsonProperty("timeStamp")]
        public string TimeStamp { get; set; }

        public TestCase() { }
    }
}
