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

        public async Task<List<User>> GetAllUsers()
        {
            return (await firebase
                .Child("Authentication")
                .OnceAsync<User>()).Select(item => new User
            {
                Mail = item.Object.Mail,
                Id = item.Object.Id,
                Password = item.Object.Password
            }).ToList();
        }

        public async Task AddUser(string Mail, string Password)
        {
            GetUserId();
            User newU = new User();
            newU.Id = userId;
            newU.Mail = Mail;
            newU.Password = Password;
            userId++;
            await firebase
                .Child("Authentication")
                .PostAsync(newU);
        }

        public async Task GetUserId()
        {
            var lastUserId = (await firebase
                .Child("Authentication")
                .OnceAsync<User>()).Last(a => a.Object.Id != null).Object.Id;

            userId = lastUserId;

        }
    }
}
