using RespectWPF.Models;

namespace RespectWPF.ViewModels
{
    // Класс обработки трансферов между клиентом и сервером
    class TransferViewModel
    {
        public TransferViewModel()
        {
            CachingData.CurrentData.UpdateData();
        }

        public async void GeneratePoints(int IdReceiver, int IdRoom, int value)
        {
            /*
             * Создание стартового баланса очков рейтинга пользователю
             */

            CachingData.CurrentData.UpdateData();

            int id = CachingData.CurrentData.Server.Points.Count + 1;

            for (int i = 0; i < CachingData.CurrentData.Server.Points.Count; i++)
            {
                if (i != CachingData.CurrentData.Server.Points[i].Id)
                {
                    id = i;
                    break;
                }
            }

            await CachingData.CurrentData.Server.AddPoints(new Models.Points()
            {
                Id = id,
                RoomId = IdRoom,
                UserId = IdReceiver,
                Value = value
            });

            CachingData.CurrentData.UpdateData();
        }

        public async void GivePoints(int IdReceiver, int IdSender, int IdRoom, int value, string description = "")
        {
            /*
             * Передача очков рейтинга пользователю в некоторой комнате
             */

            CachingData.CurrentData.UpdateData();


            foreach (var item in CachingData.CurrentData.Server.Points)
            {
                if (item.RoomId == IdRoom && IdReceiver == item.UserId)
                {
                    item.Value = item.Value + value;

                    await CachingData.CurrentData.Server.UpdatePoints(item);

                    int nid = CachingData.CurrentData.Server.Notifications.Count + 1;

                    for (int i = 0; i < CachingData.CurrentData.Server.Notifications.Count; i++)
                    {
                        if (i != CachingData.CurrentData.Server.Notifications[i].Id)
                        {
                            nid = i;
                            break;
                        }
                    }

                    await CachingData.CurrentData.Server.AddNotification(new Notification()
                    {
                        Id = nid,
                        IdReceiver = IdReceiver,
                        IdRoom = IdRoom,
                        IdSender = IdSender,
                        Value = value,
                        Description = description
                    });
                }
            }

            CachingData.CurrentData.UpdateData();
        }

        public async void PickUpPoints(int IdReceiver, int IdSender, int IdRoom, int value, string description = "")
        {
            /*
             * Cнятие очков рейтинга пользователю
             */

            CachingData.CurrentData.UpdateData();

            foreach (var item in CachingData.CurrentData.Server.Points)
            {
                if (item.RoomId == IdRoom && IdReceiver == item.UserId)
                {
                    item.Value -= value;

                    await CachingData.CurrentData.Server.UpdatePoints(item);

                    int nid = CachingData.CurrentData.Server.Notifications.Count + 1;
                    for (int i = 0; i < CachingData.CurrentData.Server.Notifications.Count; i++)
                    {
                        if (i != CachingData.CurrentData.Server.Notifications[i].Id)
                        {
                            nid = i;
                            break;
                        }
                    }

                    await CachingData.CurrentData.Server.AddNotification(new Notification()
                    {
                        Id = nid,
                        IdReceiver = IdReceiver,
                        IdRoom = IdRoom,
                        IdSender = IdSender,
                        Value = value,
                        Description = description
                    });
                }
            }

            CachingData.CurrentData.UpdateData();
        }

        public async void ResetPoints(int IdUser, int IdRoom)
        {
            /*
            * Сброс очков рейтинга пользователю
            */

            CachingData.CurrentData.UpdateData();

            foreach (var item in CachingData.CurrentData.Server.Points)
            {
                if (item.RoomId == IdRoom && IdUser == item.UserId)
                {
                    item.Value = 1000;

                    await CachingData.CurrentData.Server.UpdatePoints(item);
                }
            }

            CachingData.CurrentData.UpdateData();
        }

        public async void GiveRoots(int IdUser, int IdRoom)
        {
            /*
            * Передача прав модератора пользователю
            */

            CachingData.CurrentData.UpdateData();

            foreach (var item in CachingData.CurrentData.Server.Points)
            {
                if (item.RoomId == IdRoom && IdUser == item.UserId)
                {
                    item.Value = 500000;

                    await CachingData.CurrentData.Server.UpdatePoints(item);
                }
            }

            CachingData.CurrentData.UpdateData();
        }
    }
}
