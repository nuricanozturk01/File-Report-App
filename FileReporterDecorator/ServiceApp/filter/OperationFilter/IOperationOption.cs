using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReporterDecorator.ServiceApp.filter.OperationFilter
{
    public interface IOperationOption
    {
        public Task ApplyOperation();
    }
}
