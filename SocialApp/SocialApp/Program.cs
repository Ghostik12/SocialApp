using SocialApp.BLL.Exceptions;
using SocialApp.BLL.Models;
using SocialApp.BLL.Services;
using SocialApp.PLL.Views;
using System;
using System.ComponentModel.DataAnnotations;

namespace SocialApp
{
    class Program
    {
        static MessageService messageService;
        static UserService userService;
        public static MainView mainView;
        public static RegistrationView registrationView;
        public static AuthenticationView authenticationView;
        public static UserMenuView userMenuView;
        public static UserInfoView userInfoView;
        public static UserDataUpdateView userDataUpdateView;
        public static MessageSendingView messageSendingView;
        public static UserIncomingMessageView userIncomingMessageView;
        public static UserOutcomingMessageView userOutcomingMessageView;
        public static UserFriendView userFriendView;
        public static AddFriendView addFriendView;
        static void Main(string[] args)
        {
            userService = new UserService();
            messageService = new MessageService();

            mainView = new MainView();
            registrationView = new RegistrationView(userService);
            authenticationView = new AuthenticationView(userService);
            userMenuView = new UserMenuView(userService);
            userInfoView = new UserInfoView();
            userDataUpdateView = new UserDataUpdateView(userService);
            messageSendingView = new MessageSendingView(userService, messageService);
            userIncomingMessageView = new UserIncomingMessageView();
            userOutcomingMessageView = new UserOutcomingMessageView();
            userFriendView = new UserFriendView();
            addFriendView = new AddFriendView(userService);

            while (true)
            {
                mainView.Show();
            }
        }
    }
}