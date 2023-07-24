﻿using FileReporterDecorator.FileOperation;
namespace FileReporterAppTest.ScanTest
{
    public class CreationModifiedTestOperationTest : IClassFixture<CreationTestDataCreator>
    {
        private readonly FileOperation _scanOperation;
        private DateTime dateTime;
        public CreationModifiedTestOperationTest(CreationTestDataCreator scanTestDataCreator)
        {
            _scanOperation = scanTestDataCreator._fileOperation;
            dateTime = scanTestDataCreator._dateTime;
            _scanOperation.Run();
        }

        [Fact(DisplayName = "[1] - Check All Dates Creation Time [After]")]
        public void Valid_AllDates_CreationTime_After()
        {
            List<string> newFileList = _scanOperation.GetNewFileList().ToList();

            if (newFileList.Count() != 0) // list cannot be null
            {
                var expectedAllDatesAreValid = newFileList.Select(fi => new FileInfo(fi)).ToList().All(fi => fi.CreationTime >= dateTime);
                Assert.True(expectedAllDatesAreValid);
            }

            // If list empty
            else Assert.Empty(newFileList);
        }

        [Fact(DisplayName = "[2] - Check All Dates Creation Time [Before]")]
        public void Valid_AllDates_CreationTime_Before()
        {
            List<string> oldFileList = _scanOperation.GetOldFileList().ToList();

            if (oldFileList.Count() != 0) // list cannot be null
            {
                var expectedAllDatesAreValid = oldFileList.Select(fi => new FileInfo(fi)).ToList().All(fi => fi.CreationTime < dateTime);
                Assert.True(expectedAllDatesAreValid);
            }

            // If list empty
            else Assert.Empty(oldFileList);
        }
    }
}