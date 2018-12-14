using System;
using System.Collections.Generic;

namespace Database
{
    public static class Security
    {
        static readonly List<string> users = new List<string>();
        static readonly List<string> pass = new List<string>();

        static Security()
        {
            users.Add("Aakash");
            pass.Add("123");
        }

        public static bool LoginCheck(string username, string password)
        {
            if (!users.Contains(username) || !pass.Contains(password))
                return false;

            return users.IndexOf(username) == pass.IndexOf(password);
        }
    }
}