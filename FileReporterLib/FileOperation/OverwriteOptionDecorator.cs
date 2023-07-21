namespace FileReporterDecorator.FileOperation
{
    public class OverwriteOptionDecorator : FileOperationDecorator
    {

        public OverwriteOptionDecorator(FileOperation fileOperation) : base(fileOperation)
        {
            fileOperation.SetOverwrite(true);
            SetOverwrite(true);
        }

        public override async Task Run()
        {
            await base.Run();
        }
    }
}
