using FileReporterLib.Exceptions;

namespace FileReporterDecorator.Util
{
    public class ExceptionUtil
    {
        public static void ThrowException(Action action, Action nullReferenceExceptionAction)
        {
            try
            {
                action.Invoke();
            }
            catch (NullReferenceException ex)
            {
                nullReferenceExceptionAction.Invoke();
            }
        }


        public static void ThrowCopyAndMoveException(Action action, Action nullReferenceExceptionAction)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                nullReferenceExceptionAction.Invoke();
            }
        }

        public static void ThrowCopyConflictException(Action action, Action nullReferenceExceptionAction, bool isConflict)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                nullReferenceExceptionAction.Invoke();
                if (!isConflict)
                    throw new FileConflictException("Files are conflict!");
            }
        }

        public static void ThrowFileNotFoundException(Action action, Action finallyAction)
        {
            try
            {
                action.Invoke();
            }
            catch (FileNotFoundException ex)
            {

            }
            finally
            {
                finallyAction.Invoke();
            }
        }

    }
}
