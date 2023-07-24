using FileReporterDecorator.FileOperation;
using System.Security.AccessControl;

namespace FileReporterAppTest.CopyTest
{
    public class CopyNtfsOperationTest : IClassFixture<CopyNtfsTestDataCreator>
    {
        private readonly FileOperation _scannerOperation;
        private readonly FileOperation _copyOperation;


        public CopyNtfsOperationTest(CopyNtfsTestDataCreator copyTestDataCreator)
        {
            Directory.CreateDirectory(TEST_DIRECTORY_NTFS_PATH);

            _scannerOperation = copyTestDataCreator._scanOperation;
            _copyOperation = copyTestDataCreator._copyOperation;
            _copyOperation.Run();
        }

        [Fact(DisplayName = "[2] - Check NTFS Permissions After Copy")]
        internal void Equal_NtfsPermissions_AfterCopy()
        {
            var expectedNtfsPermissions = _scannerOperation.GetNewFileList().Select(d => new FileInfo(d)).ToList();

            var actualNtfsPermissions = GetDirectoryFileInfoArray(TEST_DIRECTORY_NTFS_PATH);

            // <a1 (before copy), a1 (after copy)>, <b1 (bc), b1 (ac)> ....
            var expectedAndActualFileInfoDictionary = ClassifySameFiles(expectedNtfsPermissions, actualNtfsPermissions);
            
            Assert.True(VerifyNtfsPermissions(expectedAndActualFileInfoDictionary));
        }


        /*
         * 
         * Check Ntfs Permissions are copied. If all of them copied, returns true. 
         * 
         */
        private bool VerifyNtfsPermissions(Dictionary<FileInfo, FileInfo> classifySameFiles)
        {
            foreach (var filePair in classifySameFiles)
            {
                FileInfo beforeFile = filePair.Key;
                FileInfo afterFile = filePair.Value;

                FileSecurity beforeFileSecurity = beforeFile.GetAccessControl();
                FileSecurity afterFileSecurity = afterFile.GetAccessControl();

                if (beforeFileSecurity.AreAccessRulesProtected || afterFileSecurity.AreAccessRulesProtected)
                {
                    return false;
                }
                else
                {
                    AuthorizationRuleCollection beforeRules = beforeFileSecurity.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
                    AuthorizationRuleCollection afterRules = afterFileSecurity.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));

                    foreach (FileSystemAccessRule beforeRule in beforeRules)
                    {
                        foreach (FileSystemAccessRule afterRule in afterRules)
                        {
                            if (beforeRule.IdentityReference.Value == afterRule.IdentityReference.Value &&
                                beforeRule.FileSystemRights == afterRule.FileSystemRights &&
                                beforeRule.AccessControlType == afterRule.AccessControlType)
                            {
                                continue;
                            }

                            else return false;
                        }
                    }
                }
            }

            return true;
        }


        /*
         * 
         * Create FileInfo Map before and after copy.
         *  <a1 (before copy), a1 (after copy)>, <b1 (bc), b1 (ac)> ....
         */
        private Dictionary<FileInfo, FileInfo> ClassifySameFiles(List<FileInfo> beforeCopyFile, FileInfo[] afterCopyFile)
        {
            return beforeCopyFile.Join(afterCopyFile, before => before.FullName,
                after => after.FullName, 
                (before, after) => new { Before = before, After = after })
                                  .Where(x => x.Before.FullName == x.After.FullName)
                                  .ToDictionary(x => x.Before, x => x.After);
        }
    }
}
