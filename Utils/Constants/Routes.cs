namespace WebApplication.Utils.Constants
{
    public static class Routes
    {
        public const string Default = "~/";
        public static class Home
        {
            public const string Index = "/homepage/index";
        }

        public static class Admin
        {
            public const string Index = "/admin/index";
            public const string Delete = "/admin/delete";
            public const string Ban = "/admin/ban";
            public const string UnBan = "/admin/unban";
        }

        public static class Account
        {
            public const string Index = "/account/index";
            public const string SignIn = "/account/sign-in";
            public const string AccessDenied = "/account/access-denied";
            public const string SignOut = "/account/sign-out";
            public const string SignUp = "/account/sign-up";
        }
    }
}
