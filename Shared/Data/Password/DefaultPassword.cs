namespace Shared.Data.Password
{
    public static class DefaultPassword
    {
        static DefaultPassword()
        {
            IsDefaultPassword = true;
            Password = "R@dif2022";
        }

        public static bool IsDefaultPassword { get; set; }
        public static string Password { get; set; }
    }
}
