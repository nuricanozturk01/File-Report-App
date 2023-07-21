using FileReporterApp.ServiceApp.options;
using FileReporterAppTest;
using FileReporterDecorator.FileOperation;
using FileReporterDecorator.FileOperation.operations;
using FileReporterDecorator.ServiceApp.filter.DateFilter;

//DEFAULT THREAD COUNT
public readonly static int DEFAULT_THREAD_COUNT = 4;

//EMPTY ACTIONS
public readonly static Action<string, string> EMPTY_SHOW_CONFLICT_MESSAGE_CALLBACK = (str1, st2) => { };
public readonly static Action<int, int, string> EMPTY_SHOW_ON_SCREEN_CALLBACK = (num, num2, str) => { };
public readonly static Action<int, TimeSpan> EMPTY_SHOW_MAXIMIZE_PROGRESSBAR_CALLBACK = (num, timeSpan) => { };
public readonly static Action EMPTY_SHOW_MIN_PROGRESSBAR_CALLBACK = () => { };
public readonly static Action<string> EMPTY_SHOW_TIME_CALLBACK = str => { };


// DIRECTORY PATH
public static readonly string TEST_DIRECTORY_PATH = "C:\\Users\\hp\\Desktop\\test_dir";
public static readonly string TEST_DIRECTORY_PATH_EMPTY = "C:\\Users\\hp\\Desktop\\test_dir_empty";
public static readonly string TEST_DIRECTORY_COPY_PATH = "C:\\Users\\hp\\Desktop\\test_dir_copy";

//WAIT N SECOND
private static readonly ManualResetEvent resetEvent = new ManualResetEvent(false);


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
    EMPTY_SHOW_MAXIMIZE_PROGRESSBAR_CALLBACK, EMPTY_SHOW_TIME_CALLBACK, EMPTY_SHOW_CONFLICT_MESSAGE_CALLBACK);

return new EmptyOperation();
}


// SCAN PROCESSES
public static class ScanBuilder
{
    public static FileOperation CreateScanProcess(DateTime dateTime, int totalFileCount, string destinationPath, IDateOption dateOpt)
    {
        return new ScanDirectoryOperation(null, dateTime, totalFileCount, DEFAULT_THREAD_COUNT, destinationPath,
        dateOpt, EMPTY_SHOW_ON_SCREEN_CALLBACK, EMPTY_SHOW_MAXIMIZE_PROGRESSBAR_CALLBACK);
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
    public static FileOperation Create_Copy_Operation(FileOperation scanProcess, int totalFileCount,
                                                            string destinationPath, string targetPath)
    {
        return CreateOperationProcess(scanProcess, Operation.COPY, totalFileCount, destinationPath, targetPath);
    }
    public static FileOperation Create_Copy_EmptyFolder_Operation(FileOperation scanProcess, int totalFileCount, string destinationPath, string targetPath)
    {
        var process = CreateOperationProcess(scanProcess, Operation.COPY, totalFileCount, destinationPath, targetPath);
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