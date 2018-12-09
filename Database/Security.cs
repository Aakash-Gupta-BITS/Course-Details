using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class Security
    {
        static readonly string[] users = new string[] { "" };
        static readonly string[] pass = new string[] { "" };
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

        public static string EncryptDecrypt(string input)
        {
            string temp = "";
            for (int i = 0; i < input.Length; ++i)
                temp += input[i] ^ users[0][i % users.Length];
            return temp;
        }
    }
}
