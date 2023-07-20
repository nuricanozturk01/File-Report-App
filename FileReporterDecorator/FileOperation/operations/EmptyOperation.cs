using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReporterDecorator.FileOperation.operations
{
    internal class EmptyOperation : FileOperation
    {
        public override async Task Run()
        {
            await Task.Run(() => { });
        }
    }
}
