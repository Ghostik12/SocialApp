using SocialApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.PLL.Views
{
    public  class UserFriendView
    {
        public void Show(IEnumerable<User> friends)
        {
            Console.WriteLine("Входящие сообщения");


            if (friends.Count() == 0)
            {
                Console.WriteLine("Входящих сообщения нет");
                return;
            }

            friends.ToList().ForEach(friend =>
            {
                Console.WriteLine("Имя друга: {0}. Фамилия друга: {1}", friend.FirstName, friend.LastName);
            });
        }
    }
}
