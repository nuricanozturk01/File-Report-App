namespace FileReporterDecorator.FileOperation
{
    internal class EmptyOptionDecorator : FileOperationDecorator
    {
        public EmptyOptionDecorator(FileOperation fileOperation) : base(fileOperation)
        {
            fileOperation.SetEmptyFolder(true);
            SetEmptyFolder(true);
        }

        public override async Task Run()
        {
            await base.Run();
        }
    }
}
