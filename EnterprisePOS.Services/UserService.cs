
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using EnterprisePOS.Data;

namespace EnterprisePOS.Services
{
    public class UserService
    {
        private readonly UserRepository repository;

        public UserService()
        {
            repository = new UserRepository();
        }

        public List<User> GetAllUsers()
        {
            return repository.GetAllUsers();
        }

        public List<User> SearchUsers(string keyword)
        {
            return repository.SearchUsers(keyword);
        }

        public User GetUserById(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid user ID.");

            return repository.GetUserById(userId);
        }

        public List<Role> GetAllRoles()
        {
            return repository.GetAllRoles();
        }

        public void AddUser(
            string username,
            string password,
            string fullName,
            int roleId)
        {
            ValidateUser(username, fullName, roleId);
            ValidatePassword(password);

            username = username.Trim();
            fullName = fullName.Trim();

            if (repository.UsernameExists(username))
                throw new InvalidOperationException(
                    "This username is already taken.");

            string salt = GenerateSalt();
            string passwordHash = HashPassword(password, salt);

            repository.AddUser(
                username,
                passwordHash,
                salt,
                fullName,
                roleId);
        }

        public void UpdateUser(
            int userId,
            string username,
            string fullName,
            int roleId,
            bool isActive)
        {
            if (userId <= 0)
                throw new ArgumentException(
                    "Please select a user to update.");

            ValidateUser(username, fullName, roleId);

            username = username.Trim();
            fullName = fullName.Trim();

            User existingUser = repository.GetUserById(userId);

            if (existingUser == null)
                throw new InvalidOperationException(
                    "User not found.");

            if (repository.UsernameExists(username, userId))
                throw new InvalidOperationException(
                    "This username is already taken.");

            repository.UpdateUser(
                userId,
                username,
                fullName,
                roleId,
                isActive);
        }

        public void ChangePassword(int userId, string newPassword)
        {
            if (userId <= 0)
                throw new ArgumentException(
                    "Please select a user.");

            ValidatePassword(newPassword);

            User existingUser = repository.GetUserById(userId);

            if (existingUser == null)
                throw new InvalidOperationException(
                    "User not found.");

            string salt = GenerateSalt();
            string passwordHash = HashPassword(newPassword, salt);

            repository.UpdatePassword(
                userId,
                passwordHash,
                salt);
        }

        public void SetUserActive(int userId, bool isActive)
        {
            if (userId <= 0)
                throw new ArgumentException(
                    "Please select a valid user.");

            User existingUser = repository.GetUserById(userId);

            if (existingUser == null)
                throw new InvalidOperationException(
                    "User not found.");

            repository.SetUserActive(userId, isActive);
        }

        private void ValidateUser(
            string username,
            string fullName,
            int roleId)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException(
                    "Username is required.");

            if (username.Trim().Length > 50)
                throw new ArgumentException(
                    "Username cannot exceed 50 characters.");

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException(
                    "Full name is required.");

            if (fullName.Trim().Length > 100)
                throw new ArgumentException(
                    "Full name cannot exceed 100 characters.");

            if (roleId <= 0)
                throw new ArgumentException(
                    "Please select a valid role.");

            bool roleExists = repository.GetAllRoles()
                .Any(r => r.RoleId == roleId);

            if (!roleExists)
                throw new ArgumentException(
                    "The selected role does not exist.");
        }

        private void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException(
                    "Password is required.");

            if (password.Length < 8)
                throw new ArgumentException(
                    "Password must contain at least 8 characters.");

            if (password.Length > 100)
                throw new ArgumentException(
                    "Password cannot exceed 100 characters.");

            foreach (char c in password)
            {
                if (c > 127)
                    throw new ArgumentException(
                        "Password must contain ASCII characters only.");
            }
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];

            using (RandomNumberGenerator rng =
                RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            return BitConverter.ToString(saltBytes)
                .Replace("-", "");
        }

        private string HashPassword(string password, string salt)
        {
            string combined = salt + password;

            byte[] input = Encoding.ASCII.GetBytes(combined);

            using (SHA256 sha = SHA256.Create())
            {
                byte[] hash = sha.ComputeHash(input);

                return BitConverter.ToString(hash)
                    .Replace("-", "");
            }
        }
    }
}
