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

        public static void ThrowCopyConflictException(Action action, Action nullReferenceExceptionAction)
        {
            try
            {
                action.Invoke();
            }
            catch (IOException ex)
            {

                nullReferenceExceptionAction.Invoke();
            }
        }
        public static void ThrowGeneralException(Action action, Action exceptionAction)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                exceptionAction.Invoke();
            }
        }

        public static void ThrowUnAuthorizedException(Action action, Action exceptionAction)
        {
            try
            {
                action.Invoke();
            }
            catch
            {
                exceptionAction.Invoke();
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
