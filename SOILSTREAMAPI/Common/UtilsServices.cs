using System.Text;
using System;

namespace SOILSTREAMAPI.Common
{
    public static class UtilsServices
    {
        private static Random random = new Random();
        public static string GenerateTrackingId()
        {
            // Generate a random alphanumeric string
            string randomString = GenerateRandomString(10);

            // Combine the ordered item's ID with the random string
            string trackingId = DateTime.Now.ToString("dd-mm-yyyy")+ $"-{randomString}";

            return trackingId;
        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder(length);

            // Generate a random string of desired length
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }
            return stringBuilder.ToString();
        }
    }
}
