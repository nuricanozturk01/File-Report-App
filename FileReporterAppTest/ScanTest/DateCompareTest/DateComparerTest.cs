using FileReporterDecorator.FileOperation;
namespace FileReporterAppTest.ScanTest
{
    public class DateComparerTest : IClassFixture<DateComparerDataCreator>
    {
        private readonly FileOperation _scanOperation;
        private readonly DateTime dateTime;

        private readonly List<string> _oldFileList;
        private readonly List<string> _newFileList;

        public DateComparerTest(DateComparerDataCreator scanTestDataCreator)
        {
            _scanOperation = scanTestDataCreator._fileOperation;
            dateTime = scanTestDataCreator._dateTime;

            _scanOperation.Run();

            _oldFileList = _scanOperation.GetOldFileList().ToList();
            _newFileList = _scanOperation.GetNewFileList().ToList();
        }










        /*
         * 
         * 
         *  Check Last Write time after the datetime object
         * 
         */
        [Fact(DisplayName = "[1] - Check All Dates Last Write Time [After]")]
        public void Valid_AllDates_LastWriteTime_After()
        {
            //var newFileList = _scanOperation.GetNewFileList().ToList();

            if (_newFileList.Count() != 0) // list cannot be empty
            {
                var expectedAllDatesAreValid = _newFileList.Select(fi => new FileInfo(fi)).ToList().All(fi => fi.LastWriteTime >= dateTime);
                Assert.True(expectedAllDatesAreValid);
            }

            // If list is empty
            else Assert.Empty(_newFileList);
        }












        /*
         * 
         * 
         *  Check Last Write time before the datetime object
         * 
        */
        [Fact(DisplayName = "[2] - Check All Dates Last Write Time [Before]")]
        public void Valid_AllDates_LastWriteTime_Before()
        {
            if (_oldFileList.Count() != 0) // list cannot be null
            {
                var expectedAllDatesAreValid = _oldFileList.Select(fi => new FileInfo(fi)).ToList().All(fi => fi.LastWriteTime < dateTime);
                Assert.True(expectedAllDatesAreValid);
            }

            // if list is empty
            else Assert.Empty(_oldFileList);
        }












        /*
         * 
         * 
         *  Check Creation time after the datetime object
         * 
        */
        [Fact(DisplayName = "[3] - Check All Dates Creation Time [After]")]
        public void Valid_AllDates_CreationTime_After()
        {
            if (_newFileList.Count() != 0) // list cannot be null
            {
                var expectedAllDatesAreValid = _newFileList.Select(fi => new FileInfo(fi)).ToList().All(fi => fi.CreationTime >= dateTime);
                Assert.True(expectedAllDatesAreValid);
            }

            // If list empty
            else Assert.Empty(_newFileList);
        }
















        /*
         * 
         * 
         *  Check Creation time before the datetime object
         * 
        */
        [Fact(DisplayName = "[4] - Check All Dates Creation Time [Before]")]
        public void Valid_AllDates_CreationTime_Before()
        {
            if (_oldFileList.Count() != 0) // list cannot be null
            {
                var expectedAllDatesAreValid = _oldFileList.Select(fi => new FileInfo(fi)).ToList().All(fi => fi.CreationTime < dateTime);
                Assert.True(expectedAllDatesAreValid);
            }

            // If list empty
            else Assert.Empty(_oldFileList);
        }













        /*
         * 
         * 
         *  Check Last Access time after the datetime object
         * 
        */
        [Fact(DisplayName = "[5] - Check All Dates Last Access Time [After]")]
        public void Valid_AllDates_LastAccessTime_After()
        {
            if (_newFileList.Count() != 0) // list cannot be null
            {
                var expectedAllDatesAreValid = _newFileList.Select(fi => new FileInfo(fi)).ToList().All(fi => fi.LastAccessTime >= dateTime);
                Assert.True(expectedAllDatesAreValid);
            }

            // If list is empty
            else Assert.Empty(_newFileList);
        }
















        /*
         * 
         * 
         *  Check Last Access time before the datetime object
         * 
        */
        [Fact(DisplayName = "[6] - Check All Dates Last Access Time [Before]")]
        public void Valid_AllDates_LastAccessTime_Before()
        {
            if (_oldFileList.Count() != 0) // list cannot be empty
            {
                var expectedAllDatesAreValid = _oldFileList.Select(fi => new FileInfo(fi)).ToList().All(fi => fi.LastAccessTime < dateTime);
                Assert.True(expectedAllDatesAreValid);
            }

            // If list is empty
            else Assert.Empty(_oldFileList);
        }
    }
}