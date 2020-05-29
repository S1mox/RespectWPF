﻿namespace RespectWPF.Models
{
    // Класс уведомлений для отображения в представлениях
    public class Notification_visual
    {
        public User Receiver { get; set; }
        public User Sender { get; set; }
        public Room Room { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }

        private ViewModels.ServerClientViewModel server = new ViewModels.ServerClientViewModel();

        public Notification_visual(Notification notification)
        {
            Setup(notification);
        }

        private async void Setup(Notification notification)
        {
            Receiver = await server.GetUser((int)notification.IdReceiver);
            Sender = await server.GetUser((int)notification.IdSender);

            Room = await server.GetRoom((int)notification.IdRoom);

            Value = (int)notification.Value;
            Description = notification.Description;
        }
    }
}
