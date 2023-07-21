using FileReporterDecorator.FileOperation;
using FileReporterDecorator.FileOperation.operations;
using FileReporterDecorator.ServiceApp.filter.DateFilter;

namespace FileReporterAppTest
{
    internal static class OperationCreatingFactory
    {
        private readonly static int DEFAULT_THREAD_COUNT = 4;
        private readonly static Action<int, int, string> EMPTY_SHOW_ON_SCREEN_CALLBACK = (num, num2, str) => { };
        private readonly static Action<int, TimeSpan> EMPTY_SHOW_MAXIMIZE_PROGRESSBAR_CALLBACK = (num, timeSpan) => { };
        private readonly static Action EMPTY_SHOW_MIN_PROGRESSBAR_CALLBACK = () => { };
        private readonly static Action<string> EMPTY_SHOW_TIME_CALLBACK = str => { };

        private static FileOperation CreateTransportProcess(FileOperation process, TransactionOperationEnum operation)
        {
            if (operation == TransactionOperationEnum.NTFS_PERMISSIONS)
                process = new NtfsSecurityOptionDecorator(process);

            if (operation == TransactionOperationEnum.EMPTY_FOLDER)
                process = new EmptyOptionDecorator(process);

            if (operation == TransactionOperationEnum.OVERWRITE)
                process = new OverwriteOptionDecorator(process);

            return process;
        }
        private static FileOperation CreateOperationProcess(FileOperation scanProcess, Operation operation, int totalFileCount,
            string destinationPath, string targetPath)
        {

            if (operation == Operation.MOVE)
                return new MoveFileOperation(scanProcess, totalFileCount,
                    DEFAULT_THREAD_COUNT, destinationPath, targetPath,
                    EMPTY_SHOW_ON_SCREEN_CALLBACK, EMPTY_SHOW_MIN_PROGRESSBAR_CALLBACK,
                    EMPTY_SHOW_TIME_CALLBACK, EMPTY_SHOW_MAXIMIZE_PROGRESSBAR_CALLBACK);

            else if (operation == Operation.COPY)
                return new CopyFileOperation(scanProcess, totalFileCount,
                    DEFAULT_THREAD_COUNT, destinationPath, targetPath,
                    EMPTY_SHOW_ON_SCREEN_CALLBACK, EMPTY_SHOW_MIN_PROGRESSBAR_CALLBACK,
                    EMPTY_SHOW_MAXIMIZE_PROGRESSBAR_CALLBACK, EMPTY_SHOW_TIME_CALLBACK,EMPTY_SHOW_CONFLICT_MESSAGE_CALLBACK);

            return new EmptyOperation();
        }


        // SCAN PROCESSES
        public static class ScanBuilder
        {
            public static FileOperation CreateScanProcess(DateTime dateTime, IDateOption dateOpt)
            {
                var totalFileCount = GetTotalFileCountOnTestDirectory();

                return new ScanDirectoryOperation(new EmptyOperation(), dateTime, totalFileCount, 
                    DEFAULT_THREAD_COUNT, TEST_DIRECTORY_PATH, dateOpt, 
                    EMPTY_SHOW_ON_SCREEN_CALLBACK, EMPTY_SHOW_MAXIMIZE_PROGRESSBAR_CALLBACK);
            }
        }
        // MOVE PROCESSES
        public static class MoveBuilder
        {
            public static FileOperation Create_Move_Operation(FileOperation scanProcess, int totalFileCount,
                                                                       string destinationPath, string targetPath)
            {
                return CreateOperationProcess(scanProcess, Operation.MOVE, totalFileCount, destinationPath, targetPath);
            }
            public static FileOperation Create_Move_EmptyFolder_Operation(FileOperation scanProcess, int totalFileCount,
                                                                          string destinationPath, string targetPath)
            {
                var moveProcess = CreateOperationProcess(scanProcess, Operation.MOVE, totalFileCount, destinationPath, targetPath);
                return CreateTransportProcess(moveProcess, TransactionOperationEnum.EMPTY_FOLDER);
            }

            public static FileOperation Create_Move_Overwrite_Operation(FileOperation scanProcess, int totalFileCount,
                                                                          string destinationPath, string targetPath)
            {
                var moveProcess = CreateOperationProcess(scanProcess, Operation.MOVE, totalFileCount, destinationPath, targetPath);
                return CreateTransportProcess(moveProcess, TransactionOperationEnum.OVERWRITE);
            }

            public static FileOperation Create_Move_EmptyFolder_And_Overwrite_Operation(FileOperation scanProcess, int totalFileCount,
                                                                          string destinationPath, string targetPath)
            {
                var moveProcess = CreateOperationProcess(scanProcess, Operation.MOVE, totalFileCount, destinationPath, targetPath);
                moveProcess = CreateTransportProcess(moveProcess, TransactionOperationEnum.EMPTY_FOLDER);
                return CreateTransportProcess(moveProcess, TransactionOperationEnum.OVERWRITE);
            }
        }
        // COPY PROCESSES
        public static class CopyBuilder
        {
            public static FileOperation Create_Copy_Operation(FileOperation scanProcess, string targetPath)
            {
                return CreateOperationProcess(scanProcess, Operation.COPY, GetTotalFileCountOnTestDirectory(), TEST_DIRECTORY_PATH, targetPath);
            }
            public static FileOperation Create_Copy_EmptyFolder_Operation(FileOperation scanProcess)
            {
                var process = CreateOperationProcess(scanProcess, Operation.COPY, GetTotalFileCountOnTestDirectory(), TEST_DIRECTORY_PATH, TEST_DIRECTORY_PATH_EMPTY);
                return CreateTransportProcess(process, TransactionOperationEnum.EMPTY_FOLDER);
            }

            public static FileOperation Create_Copy_Overwrite_Operation(FileOperation scanProcess, int totalFileCount,
                                                                          string destinationPath, string targetPath)
            {
                var process = CreateOperationProcess(scanProcess, Operation.COPY, totalFileCount, destinationPath, targetPath);
                return CreateTransportProcess(process, TransactionOperationEnum.OVERWRITE);
            }


            public static FileOperation Create_Copy_Ntfs_Permission_Operation(FileOperation scanProcess, int totalFileCount,
                                                                          string destinationPath, string targetPath)
            {
                var process = CreateOperationProcess(scanProcess, Operation.COPY, totalFileCount, destinationPath, targetPath);
                return CreateTransportProcess(process, TransactionOperationEnum.NTFS_PERMISSIONS);
            }



            public static FileOperation Create_Copy_EmptyFolder_And_Overwrite_Operation(FileOperation scanProcess, int totalFileCount,
                                                                          string destinationPath, string targetPath)
            {
                var process = CreateOperationProcess(scanProcess, Operation.COPY, totalFileCount, destinationPath, targetPath);
                process = CreateTransportProcess(process, TransactionOperationEnum.EMPTY_FOLDER);
                return CreateTransportProcess(process, TransactionOperationEnum.OVERWRITE);
            }

            public static FileOperation Create_Copy_EmptyFolder_And_Ntfs_Permission_Operation(FileOperation scanProcess, int totalFileCount,
                                                                         string destinationPath, string targetPath)
            {
                var process = CreateOperationProcess(scanProcess, Operation.COPY, totalFileCount, destinationPath, targetPath);
                process = CreateTransportProcess(process, TransactionOperationEnum.EMPTY_FOLDER);
                return CreateTransportProcess(process, TransactionOperationEnum.NTFS_PERMISSIONS);
            }
            public static FileOperation Create_Copy_Ntfs_Permission_And_Overwrite_Operation(FileOperation scanProcess, int totalFileCount,
                                                                         string destinationPath, string targetPath)
            {
                var process = CreateOperationProcess(scanProcess, Operation.COPY, totalFileCount, destinationPath, targetPath);
                process = CreateTransportProcess(process, TransactionOperationEnum.NTFS_PERMISSIONS);
                return CreateTransportProcess(process, TransactionOperationEnum.OVERWRITE);
            }
            public static FileOperation Create_Copy_EmptyFolder_And_Overwrite_And_Ntfs_Permission_Operation(FileOperation scanProcess, int totalFileCount,
                                                                         string destinationPath, string targetPath)
            {
                var process = CreateOperationProcess(scanProcess, Operation.COPY, totalFileCount, destinationPath, targetPath);
                process = CreateTransportProcess(process, TransactionOperationEnum.EMPTY_FOLDER);
                process = CreateTransportProcess(process, TransactionOperationEnum.NTFS_PERMISSIONS);
                return CreateTransportProcess(process, TransactionOperationEnum.OVERWRITE);
            }
        }
    }
}
