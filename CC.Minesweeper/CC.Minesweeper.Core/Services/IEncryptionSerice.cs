namespace CC.Minesweeper.Core.Services
{
    public interface IEncryptionSerice
    {
        /// <summary>
        /// Encrypts a value.
        /// </summary>
        /// <param name="value">The value to encrypt.</param>
        /// <returns>The encrypted value.</returns>
        string Encrypt(string value);
    }
}
