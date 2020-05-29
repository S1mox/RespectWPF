using Newtonsoft.Json;
using RespectWPF.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RespectWPF.Services
{
    class Rooms_Service
    {
        const string ServiceURI = "https://respectapi.azurewebsites.net/api/rooms";

        private HttpClient GetClient()
        {
            /*
            * Возвращает пакет подключения по HTTP к узлу API
            */

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IEnumerable<Room>> Get()
        {
            /*
             * Возвращает перечисление комнат
             */

            HttpClient client = GetClient();
            string result = await client.GetStringAsync(ServiceURI);
            return JsonConvert.DeserializeObject<IEnumerable<Room>>(result);
        }

        public async Task<Room> GetRoom(int id)
        {
            /*
            * Возвращает комнату по идентификатору
            */

            HttpClient client = GetClient();
            string result = await client.GetStringAsync(ServiceURI + "/" + id.ToString());
            return JsonConvert.DeserializeObject<Room>(result);
        }

        public async Task<Room> Add(Room room)
        {
            /*
             * Добавляет комнату на сервер исходя из переданного экземпляра
             */

            HttpClient client = GetClient();
            var response = await client.PostAsync(ServiceURI,
                new StringContent(
                    JsonConvert.SerializeObject(room),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Room>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<Room> PutRoom(Room room)
        {
            /*
            * Обновляет данные об комнате, исходя из экземпляра
            */

            HttpClient client = GetClient();
            var response = await client.PutAsync(ServiceURI + "/" + room.Id,
                new StringContent(
                    JsonConvert.SerializeObject(room),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Room>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<Room> DeleteRoom(Room room)
        {
            /*
            * Удаляет комнату по идентификатору экземпляра
            */

            HttpClient client = GetClient();

            var response = await client.DeleteAsync(ServiceURI + "/" + room.Id);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Room>(
               await response.Content.ReadAsStringAsync());
        }
    }
}
