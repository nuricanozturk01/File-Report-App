﻿using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.ScanTest
{
    public class ScanTestDataCreator : IDisposable
    {
        public FileOperation _fileOperation;
        public DateTime _dateTime;
        public ScanTestDataCreator()
        {
            _dateTime = GetTestDateTimeNew();

            //CreatedDate
            _fileOperation = ScanBuilder.CreateScanProcess(_dateTime, GetCreatedDate());

            //Last Access Date
            //_fileOperation = OperationCreatingFactory.ScanBuilder.CreateScanProcess(dateTime, totalFileCount, TEST_DIRECTORY_PATH, GetLastAccessDate());
        }
        public void Dispose()
        {

        }
    }
}
