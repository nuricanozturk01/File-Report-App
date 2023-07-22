using FileReporterDecorator.Exceptions;
using FileReporterLib.Util;
using System.Security.AccessControl;

namespace FileReporterDecorator
{
    internal class DataValidator
    {
        private readonly static int MAX_THREAD_COUNT = 25;
        public static bool ValidateData(int threadCount, string destinationPath, string targetPath, bool isReport, int _threadCount,
            NumericUpDown ThreadCounter, RadioButton CopyRadioButton, RadioButton MoveRadioButton)
        {
            try
            {
                if ((int)threadCount >= MAX_THREAD_COUNT)
                    throw new InvalidThreadCountException("Thread Count must be between 1 to 24!");

                if (destinationPath is null || string.IsNullOrEmpty(destinationPath))
                    throw new DestinationPathNullException("Destination path cannot be Empty!");

                if ((CopyRadioButton.Checked || MoveRadioButton.Checked) && !isReport)
                {
                    if (targetPath is null || string.IsNullOrEmpty(targetPath))
                        throw new TargetPathNullException("Target Path cannot be empty for Movie or Copy Operations!");

                    else
                    {
                        if (!AccessControl.HasAccessAllow(targetPath,
                            FileSystemRights.Write,
                            FileSystemRights.WriteData,
                            FileSystemRights.FullControl
                            ))
                            throw new TargetPathNullException("You do not have any permits about writing on target path!");
                    }
                }

                if (!AccessControl.HasAccessAllow(destinationPath,
                    FileSystemRights.ReadPermissions,
                    FileSystemRights.FullControl
                    ))
                    throw new UnauthorizedAccessException("you cannot access this directory!");



                return true;
            }
            catch (InvalidThreadCountException ex)
            {
                _threadCount = (int)(ThreadCounter.Value = 4);
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (DestinationPathNullException ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            catch (TargetPathNullException ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch
            {
                MessageBox.Show("Please Control the Text Boxes!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
