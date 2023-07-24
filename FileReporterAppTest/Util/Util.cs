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
        public readonly static Action<int, int, string> EMPTY_SHOW_ON_SCREEN_CALLBACK = (num, num2, str) => { };
        public readonly static Action<int, TimeSpan> EMPTY_SHOW_MAXIMIZE_PROGRESSBAR_CALLBACK = (num, timeSpan) => { };
        public readonly static Action EMPTY_SHOW_MIN_PROGRESSBAR_CALLBACK = () => { };
        public readonly static Action<string> EMPTY_SHOW_TIME_CALLBACK = str => { };
        public readonly static Action<int, TimeSpan> EMPTY_MAXIMIZE_PROGRESSBAR_CALLBACK = (num, timeSpan) => { };
        public readonly static Action<string> EMPTY_ERROR_LABEL_CALLBACK = str => { };


        private static string PATH_PREFIX = @"C:\Users\hp\Desktop\";

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


        private static IDateOption GetDateOption(bool createdDate, bool modifiedDate)
        {
            return GetSelectedDateOption(createdDate, modifiedDate);
        }


        public static DateTime GetXDayAfterFromToday(int day)
        {
            return DateTime.Now.AddDays(Math.Abs(day));
        }
        public static DateTime GetXDayBeforeFromToday(int day)
        {
            return DateTime.Now.AddDays(-Math.Abs(day));
        }
        public static int GetTotalFileCountOnDirectory(string path)
        {
            return new DirectoryInfo(path).GetFiles("*", SearchOption.AllDirectories).Length;
        }
        public static int GetTotalFileCountOnTestDirectory()
        {
            return GetTestDirectoryFileInfoArray().Length;
        }

        public static int GetTotalDirectoryCount(string path)
        {
            return Directory.GetDirectories(path, "*", SearchOption.AllDirectories).Length;
        }

        public static DateTime GetTestDateTimeNew()
        {
            return DateTime.Now;
        }

        public static IDateOption GetCreatedDate()
        {
            return GetDateOption(true, false);
        }

        public static IDateOption GetModifiedDate()
        {
            return GetDateOption(false, true);
        }

        public static IDateOption GetLastAccessDate()
        {
            return GetDateOption(false, false);
        }

        public static FileInfo[] GetTestDirectoryFileInfoArray()
        {
            return new DirectoryInfo(TEST_DIRECTORY_PATH).GetFiles("*.*", SearchOption.AllDirectories);
        }
        public static FileInfo[] GetDirectoryFileInfoArray(string path)
        {
            return new DirectoryInfo(path).GetFiles("*.*", SearchOption.AllDirectories);
        }
        public static long GetTotalByteOnTestDirectory()
        {
            return GetTestDirectoryFileInfoArray().Select(f => f.Length).Sum();
        }

        public static long GetTotalByteOnDirectory(string path)
        {
            return new DirectoryInfo(path).GetFiles("*.*", SearchOption.AllDirectories).Select(f => f.Length).Sum();
        }

        public static string[] GetDirectoriesOnPath(string path)
        {
            return Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
        }

        public static void WaitSecond(int second, Action action)
        {
            if (resetEvent.WaitOne(second * 1_000)) { }

            else action.Invoke();
        }

        public static List<string> FindEmptyDirectories(string root)
        {
            var emptyDirectories = new List<string>();

            foreach (var directory in Directory.GetDirectories(root))
            {
                if (!Directory.EnumerateFileSystemEntries(directory).Any())
                {
                    emptyDirectories.Add(directory);
                }
                else
                {
                    emptyDirectories.AddRange(FindEmptyDirectories(directory));
                }
            }

            return emptyDirectories;
        }


    }
}