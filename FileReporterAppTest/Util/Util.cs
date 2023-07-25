using FileReporterLib.Filter.DateFilter;
using static FileReportServiceLib.Util.OptionCreator;
namespace FileReporterAppTest.Util
{
    internal class Util
    {

        //DEFAULT THREAD COUNT
        public readonly static int DEFAULT_THREAD_COUNT = 12;

        //EMPTY ACTIONS
        public readonly static Action<string, string> EMPTY_SHOW_CONFLICT_MESSAGE_CALLBACK = (str1, st2) => { };
        public readonly static Action<int, string> EMPTY_SHOW_ON_SCREEN_CALLBACK = (num, str) => { };
        public readonly static Action EMPTY_SHOW_MIN_PROGRESSBAR_CALLBACK = () => { };
        public readonly static Action<string> EMPTY_SHOW_TIME_CALLBACK = str => { };
        public readonly static Action<List<string>> EMPTY_UNAUTHORIZED_REPORT_ACTION = list => { };
        public readonly static Action<int, TimeSpan> EMPTY_MAXIMIZE_PROGRESSBAR_CALLBACK = (num, timeSpan) => { };
        public readonly static Action<string> EMPTY_ERROR_LABEL_CALLBACK = str => { };
        public readonly static Action EMPTY_SHOW_PROGRESS_BAR_ACTION = () => { };


        public static string PATH_PREFIX = @"C:\Users\hp\Desktop\";

        // DIRECTORY PATH

        // [COPY]
        public static readonly string TEST_DIRECTORY_NTFS_PATH = PATH_PREFIX + "test_ntfs";
        public static readonly string TEST_DIRECTORY_PATH = PATH_PREFIX + "test_dir";
        public static readonly string TEST_DIRECTORY_PATH_EMPTY = PATH_PREFIX + "test_dir_copy_empty";
        public static readonly string TEST_DIRECTORY_COPY_PATH = PATH_PREFIX + "test_dir_copy";
        public static readonly string TEST_DIRECTORY_OVERWRITE_PATH = PATH_PREFIX + "test_dir_copy_overwrite";


        // [MOVE]
        public static readonly string BACKUP_MAIN_FILE = PATH_PREFIX + "test_dir_backup";
        public static readonly string MOVE_TEST_DIRECTORY_PATH_EMPTY = PATH_PREFIX + "test_dir_move_empty";
        public static readonly string MOVE_TEST_DIRECTORY_PATH = PATH_PREFIX + "test_dir_move";
        public static readonly string MOVE_TEST_DIRECTORY_OVERWRITE_PATH = PATH_PREFIX + "test_dir_move_overwrite";

        //WAIT N SECOND
        private static readonly ManualResetEvent resetEvent = new ManualResetEvent(false);



        /*
         * 
         * Get Selected Date Option
         * 
         */
        private static IDateOption GetDateOption(bool createdDate, bool modifiedDate)
        {
            return GetSelectedDateOption(createdDate, modifiedDate);
        }

        /*
         * 
         * Get X day after and return DateTime
         * 
         */
        public static DateTime GetXDayAfterFromToday(int day)
        {
            return DateTime.Now.AddDays(Math.Abs(day));
        }



        /*
         * 
         * Get X day before and return DateTime
         * 
         */
        public static DateTime GetXDayBeforeFromToday(int day)
        {
            return DateTime.Now.AddDays(-Math.Abs(day));
        }


        /*
         * 
         * Get Totat file count on specific directory
         * 
         */
        public static int GetTotalFileCountOnDirectory(string path)
        {
            return new DirectoryInfo(path).EnumerateFiles("*", SearchOption.AllDirectories).Count();
        }


        /*
         * 
         * Get total file count on test directory.
         * 
         */
        public static int GetTotalFileCountOnTestDirectory()
        {
            return GetTestDirectoryFileInfoArray().Length;
        }


        /*
         * 
         * Get Now date time.
         * 
         */
        public static DateTime GetTestDateTimeNew()
        {
            return DateTime.Now;
        }




        /*
         * 
         * Get Creation Date Option
         * 
         */
        public static IDateOption GetCreatedDate()
        {
            return GetDateOption(true, false);
        }


        /*
         * 
         * Get Last Modified Date Option
         * 
         */
        public static IDateOption GetModifiedDate()
        {
            return GetDateOption(false, true);
        }


        /*
         * 
         * Get Last Access Date Option
         * 
         */
        public static IDateOption GetLastAccessDate()
        {
            return GetDateOption(false, false);
        }



        /*
         * 
         * Get Files on test path and return FileInfo array. 
         * 
         */
        public static FileInfo[] GetTestDirectoryFileInfoArray()
        {
            return new DirectoryInfo(TEST_DIRECTORY_PATH).GetFiles("*.*", SearchOption.AllDirectories);
        }



        /*
         * 
         * Get Files on specific path and return FileInfo array. 
         * 
         */
        public static FileInfo[] GetDirectoryFileInfoArray(string path)
        {
            return new DirectoryInfo(path).GetFiles("*.*", SearchOption.AllDirectories);
        }


        /*
        * 
        *  Calculate the total byte on TEST directory (include subfiles)
        * 
        */
        public static long GetTotalByteOnTestDirectory()
        {
            return GetTestDirectoryFileInfoArray().Select(f => f.Length).Sum();
        }




        /*
        * 
        *  Calculate the total byte on directory (include subfiles)
        * 
        */
        public static long GetTotalByteOnDirectory(string path)
        {
            return new DirectoryInfo(path).GetFiles("*.*", SearchOption.AllDirectories).Select(f => f.Length).Sum();
        }







        // Wait N second.
        public static void WaitSecond(int second, Action action)
        {
            if (resetEvent.WaitOne(second * 1_000)) { }

            else action.Invoke();
        }




        /*
         * 
         *  Find Empty Directories on specific path and return the FullName List.
         * 
         */
        public static List<string> FindEmptyDirectories(string root)
        {
            var emptyDirectories = new List<string>();

            foreach (var directory in Directory.GetDirectories(root))
            {
                if (!Directory.EnumerateFileSystemEntries(directory).Any())
                    emptyDirectories.Add(directory);
                
                else emptyDirectories.AddRange(FindEmptyDirectories(directory));
            }

            return emptyDirectories;
        }
    }
}