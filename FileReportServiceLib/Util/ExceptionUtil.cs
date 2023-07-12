namespace FileReporterApp.Util
{
    public static class ExceptionUtil
    {
        private static void ThrowException<T>(string msg, Exception ex) where T : Exception
        {
            var constructor = typeof(T).GetConstructor(new[] { typeof(string), typeof(Exception) });

            if (constructor == null)
                throw new InvalidOperationException("Fault for exception class");

            throw (T)constructor.Invoke(new object[] { msg, ex });
        }

        public static void DoForAction<T>(Action actionCallback, string msg) where T : Exception
        {
            try
            {
                actionCallback();
            }
            catch (Exception ex)
            {
                ThrowException<T>(msg, ex);
            }
        }
        public static void DoForAction(Action actionCallback, Action exceptionMessageBox) 
        {
            try
            {
                actionCallback();
            }
            catch (Exception ex)
            {
                exceptionMessageBox();
            }
        }

        public static void DoForAction<T>(Action actionCallback, Action<Exception> exceptionHandler, string msg) where T : Exception
        {
            try
            {
                actionCallback();
            }
            catch (Exception ex)
            {
                exceptionHandler(ex);
                ThrowException<T>(msg, ex);
            }
        }

        public static R DoForFunc<T, R>(Func<R> funcCallback, string msg) where T : Exception
        {
            try
            {
                return funcCallback();
            }
            catch (Exception ex)
            {
                ThrowException<T>(msg, ex);
                return default;
            }
        }

        public static R DoForFunc<T, R>(Func<R> funcCallback, Action<Exception> exceptionHandler, string msg) where T : Exception
        {
            try
            {
                return funcCallback();
            }
            catch (Exception ex)
            {
                exceptionHandler(ex);
                ThrowException<T>(msg, ex);
                return default; // Or throw an exception depending on your use case
            }
        }

    }
}
