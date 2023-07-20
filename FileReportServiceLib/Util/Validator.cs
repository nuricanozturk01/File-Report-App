namespace FileReportServiceLib.Util
{
    public static class Validator
    {
        public static Boolean TextValidate(string text)
        {
            return !string.IsNullOrEmpty(text);
        }
    }
}
