namespace FileReporterDecorator.Util
{
    /*
     * This class written for converting (try catch) exception handling keywords to functional structure so,
     * code looks like more clear. 
     * 
     * Not good implementation but looks like more clear. 
     */

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
            catch (Exception ex)
            {
                nullReferenceExceptionAction.Invoke();
                throw new Exception();
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

        private static bool TryInvoke(Action action)
        {
            try
            {
                action.Invoke();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void ThrowUnAuthorizedException(Action action, Action exceptionAction)
        {
            try
            {
                if (!TryInvoke(action))
                    throw new UnauthorizedAccessException();
            }
            catch (UnauthorizedAccessException ex)
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
