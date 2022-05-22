using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Blockchain
{
    public enum UserRole
    {
        Admin = 1,
        User = 2
    }


    [DataContract]
    public class User : IHashable
    {
        [DataMember] public string Login { get; private set; }
        [DataMember] public UserRole Role { get; private set; }
        [DataMember] public string Password { get; private set; }
        [DataMember] public string Hash { get; private set; }

        public User() {}

        public User(string login, string password, UserRole role)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentException(nameof(login), "Логин не может быть пустым или равным null.");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException(nameof(password), "Пароль не может быть пустым или равным null.");
            }

            Login = login;
            Password = password.GetHash();
            Role = role;
            Hash = GetSummaryData().GetHash();
        }

        public string GetSummaryData()
        {
            string result = "";

            result += Login;
            result += Password;

            return result;
        }

        public string GetJson()
        {
            var jsonFormatter = new DataContractJsonSerializer(GetType());

            using (var ms = new MemoryStream())
            {
                jsonFormatter.WriteObject(ms, this);
                var jsonString = Encoding.UTF8.GetString((ms.ToArray()));
                return jsonString;
            }
        }
    }
}
