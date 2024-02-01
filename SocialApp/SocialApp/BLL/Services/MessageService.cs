using SocialApp.BLL.Exceptions;
using SocialApp.BLL.Models;
using SocialApp.DAL.Entities;
using SocialApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialApp.DAL.Repositories.MessageRepository;
using static SocialApp.DAL.Repositories.UserRepository;

namespace SocialApp.BLL.Services
{
    public class MessageService
    {
        IMessageRepository _messageRepository;
        IUserRepository _userRepository;

        public MessageService()
        {
            _messageRepository = new MessageRepository();
            _userRepository = new UserRepository();
        }

        public IEnumerable<Message> GetIncomingMessageByUserId(int recipientId)
        {
            var messages = new List<Message>();

            _messageRepository.FindByRecipientId(recipientId).ToList().ForEach(m =>
            {
                var senderUserEntity = _userRepository.FindById(m.sender_id);
                var recipientUserEntity = _userRepository.FindById(m.recipient_id);

                messages.Add(new Message(m.id, m.content, senderUserEntity.email, recipientUserEntity.email));
            });
            return messages;
        }

        public IEnumerable<Message> GetOutComingMessageByUserId(int recipientId)
        {
            var messages = new List<Message>();

            _messageRepository.FindByRecipientId(recipientId).ToList().ForEach(m =>
            {
                var senderUserEntity = _userRepository.FindById(m.sender_id);
                var recipientUserEntity = _userRepository.FindById(m.recipient_id);

                messages.Add(new Message(m.id, m.content, senderUserEntity.email, recipientUserEntity.email));
            });
            return messages;
        }
        public void SendMessage(MessageSendingData messageSendingData) 
        {
            if (String.IsNullOrEmpty(messageSendingData.Content))
                throw new ArgumentNullException();

            if (messageSendingData.Content.Length > 5000)
                throw new ArgumentOutOfRangeException();

            var findUserEntity = _userRepository.FindByEmail(messageSendingData.RecipientEmail);
            if (findUserEntity is null) throw new UserNotFoundException();

            var messageEntity = new MessageEntity()
            {
                content = messageSendingData.Content,
                sender_id = messageSendingData.SenderId,
                recipient_id = findUserEntity.id
            };

            if (_messageRepository.Create(messageEntity) == 0)
                throw new Exception();
        }
    }
}
