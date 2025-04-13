using System.Security.Cryptography;
using System.Text;

namespace ErasmusProject.Models
{
    public class PasswordHasher
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            
            using (var hmac = new HMACSHA512())
            {
                // The Key property of HMACSHA512 provides a randomly generated salt.
                passwordSalt = hmac.Key; // Assign the generated salt to the output parameter.
                // Convert the plaintext password into a byte array using UTF-8 encoding.
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                // Compute the hash of the password bytes using the HMACSHA512 instance.
                passwordHash = hmac.ComputeHash(passwordBytes); // Assign the computed hash to the output parameter.
            }
        }
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                // Convert the plaintext password into a byte array using UTF-8 encoding.
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                // Compute the hash of the password bytes using the HMACSHA512 instance initialized with the stored salt.
                byte[] computedHash = hmac.ComputeHash(passwordBytes);
                // Compare the computed hash with the stored hash byte by byte.
                // SequenceEqual ensures that both byte arrays are identical in sequence and value.
                bool hashesMatch = computedHash.SequenceEqual(storedHash);
                // Return the result of the comparison.
                return hashesMatch;
            }
        }
    }
}
