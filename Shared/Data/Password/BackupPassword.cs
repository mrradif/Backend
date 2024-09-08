namespace Shared.Data.Password
{
    public static class BackupPassword
    {
        static BackupPassword()
        {
            IsBackupPassword = true;
            Password = "R@dif2022";
        }

        public static bool IsBackupPassword { get; set; }
        public static string Password { get; set; }
    }
}
