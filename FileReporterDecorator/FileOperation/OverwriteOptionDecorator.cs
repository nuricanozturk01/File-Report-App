namespace FileReporterDecorator.FileOperation
{
    internal class OverwriteOptionDecorator : FileOperationDecorator
    {

        public OverwriteOptionDecorator(FileOperation fileOperation) : base(fileOperation)
        {
            fileOperation.SetOverwrite(true);
        }

        public override async Task Run()
        {
            await base.Run();
        }
    }
}
