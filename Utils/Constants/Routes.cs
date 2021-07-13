using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;

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
        }

        public static class Account
        {
            public const string Index = "/account/index";
            public const string Login = "/account/log-in";
            public const string AccessDenied = "/account/access-denied";
            public const string Logout = "account/log-out";
        }
    }
}
