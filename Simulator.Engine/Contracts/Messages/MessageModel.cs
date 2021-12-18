using System;

namespace Simulator.Engine.Contracts.Messages
{
    public class MessageModel : RequestSendMessage
    {
        public MessageModel()
        {
            Id = Guid.NewGuid().ToString();
            Timestamp = DateTime.Now;
        }
        public string Id { get; set; }
        public DateTime Timestamp { get; set; }

        public static MessageModel Create(RequestSendMessage request)
        {
            return new MessageModel() {Text = request.Text};
        }

        public static MessageModel Create(string text)
        {
            return new MessageModel() {Text = text};
        }

    }
}