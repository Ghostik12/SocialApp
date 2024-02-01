using SocialApp.BLL.Exceptions;
using SocialApp.BLL.Models;
using SocialApp.BLL.Services;
using SocialApp.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.PLL.Views
{
    public class AddFriendView
    {
        UserService _userService;

        public AddFriendView(UserService userService)
        {
            _userService = userService;
        }

        public void Show(User user)
        {
            try
            {
                var addFriendData = new UserAddFriendData();

                Console.Write("Введите почтовый адрес друга: ");
                addFriendData.Email = Console.ReadLine();
                addFriendData.UserId = user.Id;

                _userService.AddFrind(addFriendData);

                SuccessMessage.Show("Вы успешно добавили друга");
            }
            catch (UserNotFoundException)
            {
                AlertMessage.Show("Пользователь не найден");
            }
            catch (Exception)
            {
                AlertMessage.Show("Произошла ошибка");
            }
        }
    }
}
