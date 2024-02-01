using SocialApp.BLL.Models;
using SocialApp.BLL.Services;
using SocialApp.DAL.Repositories;
using SocialApp.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.PLL.Views
{
    public class UserDataUpdateView
    {
        UserService _userService;

        public UserDataUpdateView(UserService userService)
        {
            _userService = userService;
        }

        public void Show (User user)
        {
            Console.Write("Меня зовут:");
            user.FirstName = Console.ReadLine();

            Console.Write("Моя фамилия:");
            user.LastName = Console.ReadLine();

            Console.Write("Ссылка на моё фото:");
            user.Photo = Console.ReadLine();

            Console.Write("Мой любимый фильм:");
            user.FavoriteMovie = Console.ReadLine();

            Console.Write("Моя любимая книга:");
            user.FavoriteBook = Console.ReadLine();

            this._userService.Update(user);

            SuccessMessage.Show("Ваш профиль успешно обновлён!");
        }
    }
}
