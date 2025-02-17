using static System.Net.Mime.MediaTypeNames;

namespace Tech_Mart.Settings
{
    public static class FileSettings
    {
        public const string ImagesPath = "/assets/Admin/images";
        public const string AllowedExtensions = ".png,.jpg,.jpeg,.png";
        public const int FileMaxSizeInMB = 1;
        public const int FileMaxSizeInBytes = 1024 * 1024 * FileMaxSizeInMB;
    }
}
