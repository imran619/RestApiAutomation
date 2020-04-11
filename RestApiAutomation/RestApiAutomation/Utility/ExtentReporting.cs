using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using System;
using System.IO;

namespace RestApiAutomation.Utility
{
    public class ExtentReporting
    {
        public static ExtentReports ExtentReportDriver;
        private static ExtentReports _extent;
        private static ExtentHtmlReporter _htmlReporter;
        private static ExtentTest _testCase;
        public static object logStatus { get; private set; }
        
        public static void SetupExtentReport(string reportName, string documentTitle)
        {
            string currentTime = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
            string codebasePath = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = codebasePath.Substring(0, codebasePath.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;

            Directory.CreateDirectory(projectPath + "\\Reports\\" + "Api" + " " + currentTime);
            _htmlReporter = new ExtentHtmlReporter(projectPath + "\\Reports\\" + "Api" + " " + currentTime + "\\");
            _htmlReporter.Config.Theme = Theme.Dark;
            _htmlReporter.Config.DocumentTitle = documentTitle;
            _htmlReporter.Config.ReportName = reportName;
            _extent = new ExtentReports();
            _extent.AttachReporter(_htmlReporter);
            ExtentReportDriver = _extent;
        }
        public static void CreateTest(string testName)
        {
            _testCase = _extent.CreateTest(testName);
        }
        public static void LogReportStatement(Status status, string message)
        {
            _testCase.Log(status, message);
        }
        public static void FlushReport()
        {
            _extent.Flush();
        }       
    }
}
