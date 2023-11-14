namespace GameZone.Settings
{
    public static class FileSettings
    {
        public const string ImagePath = "/assets/images/games";
        public const string AllowedExtension = ".jpg,.jpeg,.png";
        public const int MaxFileSizeMB = 10;
        public const int MaxFileSizeByte = MaxFileSizeMB*1024*1024;
    }
}
