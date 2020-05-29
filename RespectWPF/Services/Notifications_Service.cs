using Newtonsoft.Json;
using RespectWPF.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RespectWPF.Services
{
    // Класс взаимодействия с RespectAPI -> Notifications
    class Notifications_Service
    {
        const string ServiceURI = "https://respectapi.azurewebsites.net/api/notifications";

        private HttpClient GetClient()
        {
            /*
             * Возвращает пакет подключения по HTTP к узлу API
             */

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IEnumerable<Notification>> Get()
        {
            /*
             * Возвращает перечисление уведомлений
             */

            HttpClient client = GetClient();
            string result = await client.GetStringAsync(ServiceURI);
            return JsonConvert.DeserializeObject<IEnumerable<Notification>>(result);
        }

        public async Task<Notification> GetNotification(int id)
        {
            /*
             * Возвращает уведомление по идентификатору
             */

            HttpClient client = GetClient();
            string result = await client.GetStringAsync(ServiceURI + "/" + id.ToString());
            return JsonConvert.DeserializeObject<Notification>(result);
        }

        public async Task<Notification> Add(Notification notification)
        {
            /*
             * Добавляет уведомление на сервер исходя из переданного экземпляра
             */

            HttpClient client = GetClient();
            var response = await client.PostAsync(ServiceURI,
                new StringContent(
                    JsonConvert.SerializeObject(notification),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Notification>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<Notification> PutNotification(Notification notification)
        {
            /*
            * Обновляет данные об уведомлении, исходя из экземпляра
            */

            HttpClient client = GetClient();
            var response = await client.PutAsync(ServiceURI + "/" + notification.Id,
                new StringContent(
                    JsonConvert.SerializeObject(notification),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Notification>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<Notification> DeleteNotifications(Notification notification)
        {
            /*
            * Удаляет уведомление по идентификатору экземпляра
            */

            HttpClient client = GetClient();

            var response = await client.DeleteAsync(ServiceURI + "/" + notification.Id);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Notification>(
               await response.Content.ReadAsStringAsync());
        }
    }
}
