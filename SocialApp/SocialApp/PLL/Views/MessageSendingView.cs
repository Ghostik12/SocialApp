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
    public class MessageSendingView
    {
        UserService _userService;
        MessageService _messageService;

        public MessageSendingView(UserService userService, MessageService messageService)
        {
            _userService = userService;
            _messageService = messageService;
        }

        public void Show(User user)
        {
            var messageSengingData = new MessageSendingData();

            Console.Write("Введите почтовый адрес получателя: ");
            messageSengingData.RecipientEmail = Console.ReadLine();

            Console.Write("Введите сообщение (не более 5000 символов): ");
            messageSengingData.Content = Console.ReadLine();

            messageSengingData.SenderId = user.Id;

            try
            {
                _messageService.SendMessage(messageSengingData);

                SuccessMessage.Show("Сообщение успешно отправлено");

                user = _userService.FindById(user.Id);
            }
            catch (UserNotFoundException)
            {
                AlertMessage.Show("Пользователь не найден");
            }
            catch (ArgumentNullException)
            {
                AlertMessage.Show("Введите корректное значение");
            }
            catch(Exception)
            {
                AlertMessage.Show("Произошла ошибка при отправке сообщения");
            }
        }
    }
}
