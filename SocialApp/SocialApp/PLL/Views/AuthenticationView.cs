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
    public class AuthenticationView
    {
        UserService _userService;

        public AuthenticationView(UserService userService)
        {
            _userService = userService;
        }

        public void Show()
        {
            var authenticationData = new UserAuthenticationData();

            Console.Write("Введите почтовый адрес: ");
            authenticationData.Email = Console.ReadLine();

            Console.Write("Введите пароль: ");
            authenticationData.Password = Console.ReadLine();

            try
            {
                var user = _userService.Authenticate(authenticationData);

                SuccessMessage.Show("Вы успешно вошли в социальную сеть");
                SuccessMessage.Show("Добро пожаловать " + user.FirstName);

                Program.userMenuView.Show(user);
            }
            catch (WrongPasswordException)
            {
                AlertMessage.Show("Пароль не корректный");
            }
            catch (UserNotFoundException)
            {
                AlertMessage.Show("Пользователь не найден");
            }
        }
    }
}
