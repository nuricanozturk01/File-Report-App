namespace FileReporterApp.ServiceApp.options
{
    internal class OptionConverter
    {
        public static DateOptions toDateOption(string option)
        {
            return option switch
            {
                "CreatedDate" => DateOptions.CREATED,
                "ModifiedDate" => DateOptions.MODIFIED,
                _ => DateOptions.ACCESSED
            };
        }


        public static BasicOptions toBasicOption(string option)
        {
            return option switch
            {
                "Scan" => BasicOptions.SCAN,
                "Move" => BasicOptions.MOVE,
                _ => BasicOptions.COPY
            };
        }

        public static OtherOptions toOtherOption(string option)
        {
            return option switch
            {
                "Empty Folders" => OtherOptions.EMPTY_FOLDERS,
                "NTFS Permissions" => OtherOptions.NTFS_PERMISSIONS,
                _=> OtherOptions.OVERWRITE
            };
        }

        public static List<OtherOptions> toOtherOptionList(List<string> optionList) =>
            optionList.Select(opt => toOtherOption(opt)).ToList();
    }
}
