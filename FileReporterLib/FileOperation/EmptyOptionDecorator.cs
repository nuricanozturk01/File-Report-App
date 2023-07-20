namespace FileReporterDecorator.FileOperation
{
    public class EmptyOptionDecorator : FileOperationDecorator
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
