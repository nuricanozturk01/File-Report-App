namespace FileReporterDecorator.FileOperation.operations
{
    public class EmptyOperation : FileOperation
    {
        /*
         *
         * EmptyOperation is doing nothing.
         * 
         */
        public override async Task Run() => await Task.Run(() => { });
    }
}
