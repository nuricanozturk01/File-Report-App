using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReportServiceLib.Util
{
    public interface IAction
    {
        void apply();
    }
    public interface IApplyAction : IAction
    {
        public void ApplyAction();
    }

    public interface IApplyWriterAction : IAction
    {    
        void ApplyWriterAction();
        void CreateFileAction();
    }

    public interface IInsertBufferAction : IAction
    {
        public void InsertBuffer();
    }

    public delegate void ExcelAction();
}
