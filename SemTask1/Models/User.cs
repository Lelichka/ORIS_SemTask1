using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemTask1.Models
{
    public class User
    {
        public User()
        {
        }
        public User(string userName, string email, string encyptedPassword, string guid)
        {
            UserName = userName;
            Email = email;
            EncyptedPassword = encyptedPassword;
            Guid = guid;
        }
        public User(int id,string userName, string email, string encyptedPassword, string guid)
        {
            Id = id;
            UserName = userName;
            Email = email;
            EncyptedPassword = encyptedPassword;
            Guid = guid;
        }
        [NotInsertable]
        public int Id { get; set; }
        public string? UserName { get; set; }
        
        public string Email { get; set; }
        
        public string EncyptedPassword { get; set; }
        public string Guid { get; set; }
    }
}
