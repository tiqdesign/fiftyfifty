using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiftyFifty.Models;
using Firebase.Database;
using Firebase.Database.Query;

    
namespace FiftyFifty
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://fifty-fifty-d2611.firebaseio.com/");
        private static int userId = 0;
        private User u = null;

        public async Task<List<User>> GetAllUsers()
        {
            return (await firebase
                .Child("Authentication")
                .OnceAsync<User>()).Select(item => new User
            {
                Mail = item.Object.Mail,
                Id = item.Object.Id,
                Password = item.Object.Password,
                fullName = item.Object.fullName
            }).ToList();
        }

        public async Task AddUser(string Mail, string Password, string FullName)
        {
           // userId = GetUserId();
            User newU = new User();
            newU.Id = userId;
            newU.Mail = Mail;
            newU.Password = Password;
            newU.fullName = FullName;
            userId++;
            await firebase
                .Child("Authentication")
                .PostAsync(newU);
        }

        public async Task<int> GetUserId()
        {
            var lastUserId = (await firebase
                .Child("Authentication")
                .OnceAsync<User>()).Last(a => a.Object.Id != null).Object.Id;

            return lastUserId;
        }

        public async Task<User> GetUser(string user_mail)
        {
            var have_user = (await firebase.Child("Authentication").OnceAsync<User>())
                .Any(u => u.Object.Mail == user_mail);
            if (have_user)
            {
                return (await firebase
                    .Child("Authentication")
                    .OnceAsync<User>()).First(u => u.Object.Mail == user_mail).Object;
            }
            else
            {
                return u;
            }
           
        }
    }
}
