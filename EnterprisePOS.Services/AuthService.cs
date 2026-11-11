
using System;
using System.Security.Cryptography;
using System.Text;
using EnterprisePOS.Data;

namespace EnterprisePOS.Services
{
    public class AuthService
    {
        private readonly UserRepository repository =
            new UserRepository();

        public LoginUser Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrEmpty(password))
            {
                return null;
            }

            LoginUser user =
                repository.GetUserByUsername(username.Trim());

            if (user == null || !user.IsActive)
                return null;

            if (!VerifyPassword(password, user.Salt, user.PasswordHash))
                return null;

            repository.UpdateLastLogin(user.UserId);

            return user;
        }

        private bool VerifyPassword(
            string password,
            string salt,
            string storedHash)
        {
            if (string.IsNullOrEmpty(salt) ||
                string.IsNullOrEmpty(storedHash))
            {
                return false;
            }

           
            string combined = salt + password;

            
            foreach (char c in combined)
            {
                if (c > 127)
                    return false;
            }

            byte[] input = Encoding.ASCII.GetBytes(combined);

            using (SHA256 sha = SHA256.Create())
            {
                byte[] hash = sha.ComputeHash(input);

                string calculatedHash =
                    BitConverter.ToString(hash).Replace("-", "");

                if (storedHash.Length != calculatedHash.Length)
                    return false;

                int difference = 0;

                for (int i = 0; i < calculatedHash.Length; i++)
                {
                    difference |=
                        char.ToUpperInvariant(storedHash[i]) ^
                        calculatedHash[i];
                }

                return difference == 0;
            }
        }
    }
}
