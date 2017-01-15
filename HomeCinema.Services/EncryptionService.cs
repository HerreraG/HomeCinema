using HomeCinema.Services.Abstract;
using System;
using System.Security.Cryptography;
using System.Text;

namespace HomeCinema.Services {
    public class EncryptionService : IEncryptionService {

        public string CreateSalt() {
            var data = new Byte[0x10];
            using (var cryptoServiceProvider = new RNGCryptoServiceProvider()) {
                cryptoServiceProvider.GetBytes(data);
                return Convert.ToBase64String(data);
            }
        }

        public string EncryptPassword(string password, string salt) {
            using(var sha256 = SHA256.Create()) {
                var saltedPassword = string.Format("{0}{1}", salt, password);
                byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);

                return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
            }
        }
    }
}
