namespace FileReporterDecorator.FileOperation.operations
{
    internal class EmptyOperation : FileOperation
    {
        public override async Task Run() => await Task.Run(() => { });
    }
}
