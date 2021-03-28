using CC.Minesweeper.Core.Services;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CC.Minesweeper.Infrastructure.Services
{
    /// <inheritdoc/>
    public class EncryptionService : IEncryptionSerice
    {
        /// <inheritdoc/>
        public string Encrypt(string value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
    }
}
