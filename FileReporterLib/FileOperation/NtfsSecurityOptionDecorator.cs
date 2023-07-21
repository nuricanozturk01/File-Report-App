namespace FileReporterDecorator.FileOperation
{
    public class NtfsSecurityOptionDecorator : FileOperationDecorator
    {

        public NtfsSecurityOptionDecorator(FileOperation fileOperation) : base(fileOperation)
        {
            SetNtfsPermissionAction((sourceFileInfo, targetFileInfo) => CopyNtfsPermissions(sourceFileInfo, targetFileInfo));
            SetNtfsPermissions(true);

            fileOperation.SetNtfsPermissions(true);
            fileOperation.SetNtfsPermissionAction((sourceFileInfo, targetFileInfo) => CopyNtfsPermissions(sourceFileInfo, targetFileInfo));
        }
        private void CopyNtfsPermissions(FileInfo sourceFile, FileInfo targetFile)
        {
            var security = sourceFile.GetAccessControl();
            security.SetAccessRuleProtection(true, true);
            targetFile.SetAccessControl(security);
        }
        public override async Task Run()
        {
            await base.Run();
        }
    }
}
