using System;
using System.Globalization;

namespace Core
{
    public static class StringFormatter
    {
        public static string AddCurrentTimeToString(string userName)
        {
            return DateTime.Now.ToString(CultureInfo.InvariantCulture) + " Hello, " + userName + "!";
        }
    }
}
