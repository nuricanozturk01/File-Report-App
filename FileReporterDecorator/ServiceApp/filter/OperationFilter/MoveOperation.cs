namespace FileReporterDecorator.ServiceApp.filter.OperationFilter
{
    internal class MoveOperation : IOperationOption
    {
        private readonly Func<int, Task> _action;
        private readonly int _fileCount;
        public MoveOperation(Func<int, Task> function, int totalFileCount)
        {
            _action = function;
            _fileCount = totalFileCount;
        }

        public async Task ApplyOperation()
        {
            await _action.Invoke(_fileCount);
        }
    }
}
