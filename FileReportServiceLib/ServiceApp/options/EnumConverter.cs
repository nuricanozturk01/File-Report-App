namespace FileReporterApp.ServiceApp.options
{
    public class EnumConverter
    {
        public static DateOptions ToDateOption(string option)
        {
            return option switch
            {
                "CreatedDate" => DateOptions.CREATED,
                "ModifiedDate" => DateOptions.MODIFIED,
                _ => DateOptions.ACCESSED
            };
        }


        public static BasicOptions ToBasicOption(string option)
        {
            return option switch
            {
                "Scan" => BasicOptions.SCAN,
                "Move" => BasicOptions.MOVE,
                _ => BasicOptions.COPY
            };
        }

        public static OtherOptions ToOtherOption(string option)
        {
            return option switch
            {
                "Empty Folders" => OtherOptions.EMPTY_FOLDERS,
                "NTFS Permissions" => OtherOptions.NTFS_PERMISSIONS,
                _=> OtherOptions.OVERWRITE
            };
        }

        public static List<OtherOptions> ToOtherOptionList(List<string> optionList) =>
            optionList.Select(opt => ToOtherOption(opt)).ToList();

        public static FileType toFileType(int filterIndex)
        {
            return filterIndex == 1 ? FileType.TEXT : FileType.EXCEL;
        }
    }
}
