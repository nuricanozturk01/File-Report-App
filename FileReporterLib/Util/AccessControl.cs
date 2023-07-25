using System.Security.AccessControl;
using System.Security.Principal;

namespace FileReporterLib.Util
{
    public static class AccessControl
    {
        /*
         * 
         * This method written for control the access permits for directories 
         * 
         */
        public static bool HasAccessAllow(string folder, params FileSystemRights[] rights)
        {
            bool hasAccess = false;

            // Get Current User NTAccount Name
            string executingUser = WindowsIdentity.GetCurrent().Name;

            NTAccount acc = new NTAccount(executingUser);
#pragma warning disable CS8600 
            SecurityIdentifier secId = acc.Translate(typeof(SecurityIdentifier)) as SecurityIdentifier;
#pragma warning restore CS8600 

            var dirInfo = new DirectoryInfo(folder);

            DirectorySecurity dirSec = dirInfo.GetAccessControl();

            AuthorizationRuleCollection authRules = dirSec.GetAccessRules(true, true, typeof(SecurityIdentifier));

            foreach (FileSystemAccessRule ar in authRules)
            {
#pragma warning disable CS8602
                if (secId.CompareTo(ar.IdentityReference as SecurityIdentifier) == 0)
                {
                    var fileSystemRights = ar.FileSystemRights;

                    if (AreAllTrue(rights, fileSystemRights))
                        hasAccess = true;
                }
#pragma warning restore CS8602
            }
            return hasAccess;
        }







        /*
         * 
         * returns the if least one valid, true.
         * 
         */
        private static bool AreAllTrue(FileSystemRights[] rights, FileSystemRights fileSystemRights)
        {
            foreach (var right in rights)
                if (right == fileSystemRights)
                    return true;

            return false;
        }
    }
}