using RespectWPF.CachingData;
using RespectWPF.Models;

namespace RespectWPF.ViewModels
{
    class RootsViewModel
    {
        public bool IsModerator(Room currentRoom)
        {
            /*
             * Возвращает True, если текущий пользователь является модератором комнаты
             * Иначе - False
             */

            CurrentData.UpdateData();
            int userId = CurrentData.CurrentUser.Id;

            foreach (var item in CurrentData.Server.Points)
            {
                if (item.UserId == userId && item.RoomId == currentRoom.Id)
                {
                    if (item.Value == 500000)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsMember(Room currentRoom)
        {
            /*
            * Возвращает True, если текущий пользователь является участником комнаты
            * Иначе - False
            */

            CurrentData.UpdateData();
            int userId = CurrentData.CurrentUser.Id;

            foreach (var item in CurrentData.Server.Points)
            {
                if (item.UserId == userId && item.RoomId == currentRoom.Id)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsCurrentUser(User profile_user)
        {
            /*
            * Возвращает True, если текущий пользователь является носителем своего идентификтаора
            * Иначе - False
            */

            if (CachingData.CurrentData.CurrentUser.Id == profile_user.Id)
            {
                return true;
            }

            return false;
        }
    }
}
