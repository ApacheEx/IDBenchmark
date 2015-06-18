using System;
using System.Security.Cryptography;
using System.Text;

namespace IDBenchmark
{
    class Hash
    {
        public Hash() { }

        public enum HashType : int
        {
            MD5,
            SHA1,
            SHA256,
            SHA512
        }

        public static string GetHash(string text, HashType hashType)
        {
            string hashString;
            switch (hashType) {
                case HashType.MD5:
                    hashString = GetMD5(text);
                    break;
                case HashType.SHA1:
                    hashString = GetSHA1(text);
                    break;
                case HashType.SHA256:
                    hashString = GetSHA256(text);
                    break;
                case HashType.SHA512:
                    hashString = GetSHA512(text);
                    break;
                default:
                    hashString = "Invalid Hash Type";
                    break;
            }
            return hashString;
        }

        public static bool CheckHash(string original, string hashString, HashType hashType)
        {
            var originalHash = GetHash(original, hashType);
            return (originalHash == hashString);
        }

        private static string GetMD5(string text)
        {
            var ue = new UnicodeEncoding();
            var message = ue.GetBytes(text);

            var hashString = new MD5CryptoServiceProvider();
            var hex = "";

            var hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue) {
                hex += $"{x:x2}";
            }
            return hex;
        }

        private static string GetSHA1(string text)
        {
            var ue = new UnicodeEncoding();
            var message = ue.GetBytes(text);

            var hashString = new SHA1Managed();
            var hex = "";

            var hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue) {
                hex += $"{x:x2}";
            }
            return hex;
        }

        private static string GetSHA256(string text)
        {
            var ue = new UnicodeEncoding();
            var message = ue.GetBytes(text);

            var hashString = new SHA256Managed();
            var hex = "";

            var hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue) {
                hex += $"{x:x2}";
            }
            return hex;
        }

        private static string GetSHA512(string text)
        {
            var ue = new UnicodeEncoding();
            var message = ue.GetBytes(text);

            var hashString = new SHA512Managed();
            var hex = "";

            var hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue) {
                hex += $"{x:x2}";
            }
            return hex;
        }
    }
}
