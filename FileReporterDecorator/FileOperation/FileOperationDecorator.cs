using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReporterDecorator.FileOperation
{
    internal abstract class FileOperationDecorator : FileOperation
    {
        protected FileOperation _fileOperation;

        public FileOperationDecorator(FileOperation fileOperation)
        {
            _fileOperation = fileOperation;
      
        }

        /*public void SetNtfsPermissions(bool ntfs) => _fileOperation.SetNtfsPermissions(ntfs);
        public void SetEmptyFolder(bool emptyFolder) => _fileOperation.SetEmptyFolder(emptyFolder);
        public void SetOverwrite(bool overwrite) => _fileOperation.SetOverwrite(overwrite);*/


        public override async Task Run()
        {
            await _fileOperation.Run();
        }

    }
}
