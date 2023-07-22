using FileReporterDecorator.FileOperation;
using FileReporterDecorator.FileOperation.operations;
using FileReporterLib.Filter.DateFilter;

namespace FileReporterAppTest
{
    internal static class OperationCreatingFactory
    {
        
        private static FileOperation CreateTransportProcess(FileOperation process, TransactionOperationEnum operation, FileOperation scanProcess)
        {
            if (operation == TransactionOperationEnum.NTFS_PERMISSIONS)
                process = new NtfsSecurityOptionDecorator(process, scanProcess);

            if (operation == TransactionOperationEnum.EMPTY_FOLDER)
                process = new EmptyOptionDecorator(process, scanProcess);

            if (operation == TransactionOperationEnum.OVERWRITE)
                process = new OverwriteOptionDecorator(process, scanProcess);

            return process;
        }
        private static FileOperation CreateOperationProcess(FileOperation scanProcess, OperationEnum operation, int totalFileCount,
                                                            string destinationPath, string targetPath)
        {

            if (operation == OperationEnum.MOVE)
                return new MoveFileOperation(scanProcess, totalFileCount,
                    DEFAULT_THREAD_COUNT, destinationPath, targetPath,
                    EMPTY_SHOW_ON_SCREEN_CALLBACK, EMPTY_SHOW_MIN_PROGRESSBAR_CALLBACK,
                    EMPTY_SHOW_TIME_CALLBACK, EMPTY_MAXIMIZE_PROGRESSBAR_CALLBACK,
                    EMPTY_ERROR_LABEL_CALLBACK);

            else if (operation == OperationEnum.COPY)
                return new CopyFileOperation(scanProcess, totalFileCount,
                    DEFAULT_THREAD_COUNT, destinationPath, targetPath,
                    EMPTY_SHOW_ON_SCREEN_CALLBACK, EMPTY_SHOW_MIN_PROGRESSBAR_CALLBACK,
                    EMPTY_MAXIMIZE_PROGRESSBAR_CALLBACK, EMPTY_SHOW_TIME_CALLBACK,
                    EMPTY_ERROR_LABEL_CALLBACK);

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
                                                  EMPTY_SHOW_ON_SCREEN_CALLBACK, EMPTY_MAXIMIZE_PROGRESSBAR_CALLBACK);
            }
        }
        // MOVE PROCESSES
        public static class MoveBuilder
        {
            public static FileOperation Create_Move_Operation(FileOperation scanProcess, string targetPath)
            {
                var totalFileCount = GetTotalFileCountOnTestDirectory();

                return CreateOperationProcess(scanProcess, OperationEnum.MOVE, totalFileCount, TEST_DIRECTORY_PATH, targetPath);
            }
            public static FileOperation Create_Move_EmptyFolder_Operation(FileOperation scanProcess)
            {
                var moveProcess = CreateOperationProcess(scanProcess, OperationEnum.MOVE, GetTotalFileCountOnTestDirectory(),
                                                         TEST_DIRECTORY_PATH, MOVE_TEST_DIRECTORY_PATH_EMPTY);

                return CreateTransportProcess(moveProcess, TransactionOperationEnum.EMPTY_FOLDER, scanProcess);
            }

            public static FileOperation Create_Move_Overwrite_Operation(FileOperation scanProcess, string targetPath)
            {
                var moveProcess = CreateOperationProcess(scanProcess, OperationEnum.MOVE, GetTotalFileCountOnTestDirectory(),
                                                         TEST_DIRECTORY_PATH, targetPath);

                return CreateTransportProcess(moveProcess, TransactionOperationEnum.OVERWRITE, scanProcess);
            }
        }
        // COPY PROCESSES
        public static class CopyBuilder
        {
            public static FileOperation Create_Copy_Operation(FileOperation scanProcess, string targetPath)
            {
                return CreateOperationProcess(scanProcess, OperationEnum.COPY, GetTotalFileCountOnTestDirectory(),
                                              TEST_DIRECTORY_PATH, targetPath);
            }

            public static FileOperation Create_Copy_Operation(FileOperation scanProcess, string destinationPath, string targetPath)
            {
                var totalFile = GetTotalFileCountOnTestDirectory();
                return CreateOperationProcess(scanProcess, OperationEnum.COPY, totalFile, destinationPath, targetPath);
            }
            public static FileOperation Create_Copy_EmptyFolder_Operation(FileOperation scanProcess)
            {
                var process = CreateOperationProcess(scanProcess, OperationEnum.COPY, GetTotalFileCountOnTestDirectory(),
                                                     TEST_DIRECTORY_PATH, TEST_DIRECTORY_PATH_EMPTY);

                return CreateTransportProcess(process, TransactionOperationEnum.EMPTY_FOLDER, scanProcess);
            }

            public static FileOperation Create_Copy_Overwrite_Operation(FileOperation scanProcess, string targetPath)
            {
                var process = CreateOperationProcess(scanProcess, OperationEnum.COPY, GetTotalFileCountOnTestDirectory(),
                                                     TEST_DIRECTORY_PATH, targetPath);

                return CreateTransportProcess(process, TransactionOperationEnum.OVERWRITE, scanProcess);
            }

            public static FileOperation Create_Copy_Ntfs_Permission_Operation(FileOperation scanProcess, string targetPath)
            {
                var process = CreateOperationProcess(scanProcess, OperationEnum.COPY, GetTotalFileCountOnTestDirectory(),
                                                     TEST_DIRECTORY_PATH, targetPath);

                return CreateTransportProcess(process, TransactionOperationEnum.NTFS_PERMISSIONS, scanProcess);
            }
        }
    }
}
