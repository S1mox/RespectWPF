using Newtonsoft.Json;
using RespectWPF.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RespectWPF.Services
{
    class Users_Service
    {
        const string ServiceURI = "https://respectapi.azurewebsites.net/api/users";

        private HttpClient GetClient()
        {
            /*
            * Возвращает пакет подключения по HTTP к узлу API
            */

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IEnumerable<User>> Get()
        {
            /*
            * Возвращает перечисление пользователей
            */

            HttpClient client = GetClient();
            string result = await client.GetStringAsync(ServiceURI);
            return JsonConvert.DeserializeObject<IEnumerable<User>>(result);
        }

        public async Task<User> GetUser(int id)
        {
            /*
            * Возвращает пользователя по идентификатору
            */

            HttpClient client = GetClient();
            string result = await client.GetStringAsync(ServiceURI + "/" + id.ToString());
            return JsonConvert.DeserializeObject<User>(result);
        }

        public async Task<User> Add(User user)
        {
            /*
            * Добавляет пользователя на сервер исходя из переданного экземпляра
            */

            HttpClient client = GetClient();
            var response = await client.PostAsync(ServiceURI,
                new StringContent(
                    JsonConvert.SerializeObject(user),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<User>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<User> PutUser(User user)
        {
            /*
            * Обновляет данные об пользователе, исходя из экземпляра
            */

            HttpClient client = GetClient();
            var response = await client.PutAsync(ServiceURI + "/" + user.Id,
                new StringContent(
                    JsonConvert.SerializeObject(user),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<User>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<User> DeleteUser(User user)
        {
            /*
            * Удаляет пользователя по идентификатору экземпляра
            */

            HttpClient client = GetClient();

            var response = await client.DeleteAsync(ServiceURI + "/" + user.Id);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<User>(
               await response.Content.ReadAsStringAsync());
        }
    }
}
