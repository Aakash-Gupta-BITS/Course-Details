using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class Security
    {
        static readonly string[] users = new string[] { "Aakash" };
        static readonly string[] pass = new string[] { "123" };
        public static bool LoginCheck(string username, string password)
        {
            if (!users.Contains(username) || !pass.Contains(password))
                return false;

            int index = 0;
            for (int i = 0; i < users.Length; ++i)
                if (users[i] == username)
                {
                    index = i;
                    break;
                }

            if (password == pass[index])
                return true;
            else
                return false;
        }
    }
}
