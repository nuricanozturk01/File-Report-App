namespace FileReporterDecorator.FileOperation
{
    public class NtfsSecurityOptionDecorator : FileOperationDecorator
    {
        private readonly FileOperation _fileOperation;
        private readonly FileOperation scanProcess;

        public NtfsSecurityOptionDecorator(FileOperation fileOperation, FileOperation scanProcess)
        {

            _fileOperation = fileOperation;
            this.scanProcess = scanProcess;
            SetNtfsPermissionAction((sourceFileInfo, targetFileInfo) => CopyNtfsPermissions(sourceFileInfo, targetFileInfo));
            SetNtfsPermissions(true);
            _fileOperation.SetNtfsPermissions(true);
            _fileOperation.SetNtfsPermissionAction((sourceFileInfo, targetFileInfo) => CopyNtfsPermissions(sourceFileInfo, targetFileInfo));
            scanProcess.SetNtfsPermissions(true);
            scanProcess.SetNtfsPermissionAction((sourceFileInfo, targetFileInfo) => CopyNtfsPermissions(sourceFileInfo, targetFileInfo));
        }
        /*
         * 
         * Set  and move NTFS permissions destination file to target file 
         * 
         */
        private void CopyNtfsPermissions(FileInfo sourceFile, FileInfo targetFile)
        {
            var security = sourceFile.GetAccessControl();
            security.SetAccessRuleProtection(true, true);
            targetFile.SetAccessControl(security);
        }

        /*
         * 
         * Trigger method for decorate the Ntfs Security option 
         * 
         */
        public override async Task Run()
        {
            _fileOperation.SetNtfsPermissions(true);
            _fileOperation.SetNtfsPermissionAction((sourceFileInfo, targetFileInfo) => CopyNtfsPermissions(sourceFileInfo, targetFileInfo));
            scanProcess.SetNtfsPermissions(true);
            scanProcess.SetNtfsPermissionAction((sourceFileInfo, targetFileInfo) => CopyNtfsPermissions(sourceFileInfo, targetFileInfo));
            await _fileOperation.Run();
        }
    }
}
