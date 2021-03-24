using CC.Minesweeper.Core.Services;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CC.Minesweeper.Infrastructure.Services
{
    public class EncryptionService : IEncryptionSerice
    {
        public string Encrypt(string value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
    }
}
