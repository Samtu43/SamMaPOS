
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EnterprisePOS.Data
{
    public class LoginUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }

    public class UserRepository
    {
        public LoginUser GetUserByUsername(string username)
        {
            using (var db = new PosDBDataDataContext())
            using (var command = db.Connection.CreateCommand())
            {
                command.CommandText = "dbo.usp_Login";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@Username";
                parameter.DbType = DbType.String;
                parameter.Size = 50;
                parameter.Value = username;

                command.Parameters.Add(parameter);

                db.Connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                        return null;

                    return new LoginUser
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        Username = Convert.ToString(reader["Username"]),
                        PasswordHash = Convert.ToString(reader["PasswordHash"]),
                        Salt = Convert.ToString(reader["Salt"]),
                        FullName = Convert.ToString(reader["FullName"]),
                        RoleId = Convert.ToInt32(reader["RoleId"]),
                        RoleName = Convert.ToString(reader["RoleName"]),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    };
                }
            }
        }

        public void UpdateLastLogin(int userId)
        {
            using (var db = new PosDBDataDataContext())
            using (var command = db.Connection.CreateCommand())
            {
                command.CommandText = "dbo.usp_User_TouchLogin";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@UserId";
                parameter.DbType = DbType.Int32;
                parameter.Value = userId;

                command.Parameters.Add(parameter);

                db.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<User> GetAllUsers()
        {
            using (var db = new PosDBDataDataContext())
            {
                return db.Users
                    .OrderBy(u => u.Username)
                    .ToList();
            }
        }

        public List<User> SearchUsers(string keyword)
        {
            using (var db = new PosDBDataDataContext())
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return db.Users
                        .OrderBy(u => u.Username)
                        .ToList();
                }

                keyword = keyword.Trim();

                return db.Users
                    .Where(u =>
                        u.Username.Contains(keyword) ||
                        u.FullName.Contains(keyword))
                    .OrderBy(u => u.Username)
                    .ToList();
            }
        }

        public User GetUserById(int userId)
        {
            using (var db = new PosDBDataDataContext())
            {
                return db.Users
                    .FirstOrDefault(u => u.UserId == userId);
            }
        }

        public bool UsernameExists(string username, int excludeUserId = 0)
        {
            using (var db = new PosDBDataDataContext())
            {
                return db.Users.Any(u =>
                    u.Username == username &&
                    u.UserId != excludeUserId);
            }
        }

        public List<Role> GetAllRoles()
        {
            using (var db = new PosDBDataDataContext())
            {
                return db.Roles
                    .OrderBy(r => r.RoleName)
                    .ToList();
            }
        }

        public void AddUser(
            string username,
            string passwordHash,
            string salt,
            string fullName,
            int roleId)
        {
            using (var db = new PosDBDataDataContext())
            {
                User user = new User
                {
                    Username = username,
                    PasswordHash = passwordHash,
                    Salt = salt,
                    FullName = fullName,
                    RoleId = roleId,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                db.Users.InsertOnSubmit(user);
                db.SubmitChanges();
            }
        }

        public void UpdateUser(
            int userId,
            string username,
            string fullName,
            int roleId,
            bool isActive)
        {
            using (var db = new PosDBDataDataContext())
            {
                User user = db.Users
                    .FirstOrDefault(u => u.UserId == userId);

                if (user == null)
                {
                    throw new InvalidOperationException(
                        "User not found.");
                }

                user.Username = username;
                user.FullName = fullName;
                user.RoleId = roleId;
                user.IsActive = isActive;

                db.SubmitChanges();
            }
        }

        public void UpdatePassword(
            int userId,
            string passwordHash,
            string salt)
        {
            using (var db = new PosDBDataDataContext())
            {
                User user = db.Users
                    .FirstOrDefault(u => u.UserId == userId);

                if (user == null)
                {
                    throw new InvalidOperationException(
                        "User not found.");
                }

                user.PasswordHash = passwordHash;
                user.Salt = salt;

                db.SubmitChanges();
            }
        }

        public void SetUserActive(int userId, bool isActive)
        {
            using (var db = new PosDBDataDataContext())
            {
                User user = db.Users
                    .FirstOrDefault(u => u.UserId == userId);

                if (user == null)
                {
                    throw new InvalidOperationException(
                        "User not found.");
                }

                user.IsActive = isActive;

                db.SubmitChanges();
            }
        }

        public bool UserHasOrders(int userId)
        {
            using (var db = new PosDBDataDataContext())
            {
                return db.Orders.Any(o => o.UserId == userId);
            }
        }
    }
}
