using SocialApp.BLL.Exceptions;
using SocialApp.BLL.Models;
using SocialApp.DAL.Entities;
using SocialApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialApp.DAL.Repositories.FriendRepository;
using static SocialApp.DAL.Repositories.UserRepository;

namespace SocialApp.BLL.Services
{
    public class UserService
    {
        MessageService messageService;
        IUserRepository userRepository;
        IFriendRepository friendRepository;

        public UserService() 
        {
            userRepository = new UserRepository();
            friendRepository = new FriendRepository();
            messageService = new MessageService();
        }
        public void Register(UserRegistrationData userRegistrationData)
        {
            if (String.IsNullOrEmpty(userRegistrationData.FirstName))
                throw new ArgumentNullException();

            if (String.IsNullOrEmpty(userRegistrationData.LastName))
                throw new ArgumentNullException();

            if (String.IsNullOrEmpty(userRegistrationData.Password) || userRegistrationData.Password.Length < 8)
                throw new ArgumentNullException();

            if (String.IsNullOrEmpty(userRegistrationData.Email) || !new EmailAddressAttribute().IsValid(userRegistrationData.Email))
                throw new ArgumentNullException();

            if (userRepository.FindByEmail(userRegistrationData.Email) != null)
                throw new ArgumentNullException();

            var userEntity = new UserEntity()
            {
                firstname = userRegistrationData.FirstName,
                lastname = userRegistrationData.LastName,
                password = userRegistrationData.Password,
                email = userRegistrationData.Email,
            };

            if (this.userRepository.Create(userEntity) == 0)
                throw new Exception();
        }

        public User Authenticate(UserAuthenticationData userAuthenticationData)
        {
            var findUserEntity = userRepository.FindByEmail(userAuthenticationData.Email);
            if (findUserEntity is null) throw new UserNotFoundException();

            if (findUserEntity.password != userAuthenticationData.Password)
                throw new WrongPasswordException();

            return ConstructUserModel(findUserEntity);
        }

        public User FindByEmail(string email)
        {
            var findUserEntity = userRepository.FindByEmail(email);
            if (findUserEntity is null) throw new UserNotFoundException();

            return ConstructUserModel(findUserEntity);
        }

        public User FindById(int id)
        {
            var findUserEntity = userRepository.FindById(id);
            if (findUserEntity is null) throw new UserNotFoundException();

            return ConstructUserModel(findUserEntity);
        }

        public void Update(User user)
        {
            var updatableUserEntity = new UserEntity()
            {
                id = user.Id,
                firstname = user.FirstName,
                lastname = user.LastName,
                password = user.Password,
                email = user.Email,
                photo = user.Photo,
                favorite_movie = user.FavoriteMovie,
                favorite_book = user.FavoriteBook
            };

            if (this.userRepository.Update(updatableUserEntity) == 0)
                throw new Exception();
        }

        public IEnumerable<User>GetFriendsByUserId (int userId)
        {
            return friendRepository.FindAllByUserId(userId)
                .Select(friendEntity => FindById(friendEntity.friend_id));
        }

        public void AddFrind(UserAddFriendData userAddFriendData)
        {
            var findFriend = userRepository.FindByEmail(userAddFriendData.Email);
            if (findFriend is null) throw new UserNotFoundException();

            var friend = new FriendEntity()
            {
                user_id = userAddFriendData.UserId,
                friend_id = findFriend.id
            };

            if (this.friendRepository.Create(friend) == 0) throw new Exception();
        }

        private User ConstructUserModel(UserEntity userEntity)
        {
            var incomingMessages = messageService.GetIncomingMessageByUserId(userEntity.id);
            var outgoingMessages = messageService.GetOutComingMessageByUserId(userEntity.id);
            var friends = GetFriendsByUserId(userEntity.id);

            return new User(userEntity.id,
                          userEntity.firstname,
                          userEntity.lastname,
                          userEntity.password,
                          userEntity.email,
                          userEntity.photo,
                          userEntity.favorite_movie,
                          userEntity.favorite_book, 
                          incomingMessages,
                          outgoingMessages,
                          friends);
        }
    }
}
