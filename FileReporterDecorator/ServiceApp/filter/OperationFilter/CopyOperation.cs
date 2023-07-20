using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReporterDecorator.ServiceApp.filter.OperationFilter
{
    internal class CopyOperation : IOperationOption
    {
        private readonly Func<int, Task> _copyAction;
        private readonly int _totalFileCount;
        public CopyOperation(Func<int, Task> function, int totalFileCount)
        {
            _copyAction = function;
            _totalFileCount = totalFileCount;
        }

        public async Task ApplyOperation()
        {
            await Task.Run(() => _copyAction.Invoke(_totalFileCount));
        }
    }
}
