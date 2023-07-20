namespace FileReporterDecorator.FileOperation.operations
{
    public class EmptyOperation : FileOperation
    {
        public override async Task Run() => await Task.Run(() => { });
    }
}
