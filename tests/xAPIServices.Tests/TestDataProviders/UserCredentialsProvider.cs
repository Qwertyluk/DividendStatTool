using Common;
using System;
using System.Security.Cryptography;
using System.Text;

namespace xAPIServices.Tests.TestDataProviders
{
    internal static class UserCredentialsProvider
    {
        public static UserCredentials GetUserCredentials(string filePath)
        {
            UserCredentials? obj = new JsonFileReader().GetObject<UserCredentials>(filePath);
            if (obj == null)
            {
                throw new InvalidOperationException($"Couldn't read user credentials from file {filePath}");
            }
            byte[] passwordInBytes = Convert.FromBase64String(obj.Password);
            byte[] decryptedData = ProtectedData.Unprotect(passwordInBytes, null, DataProtectionScope.LocalMachine);
            obj.Password = Encoding.UTF8.GetString(decryptedData);

            return obj;
        }
    }
}
