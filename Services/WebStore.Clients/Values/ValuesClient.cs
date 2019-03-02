using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Interfaces.Api;

namespace WebStore.Clients.Values
{
    public class ValuesClient : BaseClient, IValuesService
    {
        public ValuesClient(IConfiguration configuration) : base(configuration)
        {
            ServicesAddress = "api/values";  //адрес сервиса, values - контроллер
        }

        public IEnumerable<string> Get() => GetAsync().Result;

        public async Task<IEnumerable<string>> GetAsync()
        {
            var list = new List<string>();
            //запрос
            var response = await _client.GetAsync($" {ServicesAddress} ");
            //успешный результат
            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadAsAsync<List<string>>();
            }
            return list;
        }

        public string Get(int id) => GetAsync(id).Result;

        public async Task<string> GetAsync(int id)
        {
            var result = string.Empty;
            var response = await _client.GetAsync($"{ServicesAddress}/get/{id}");
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<string>();
            }
            return result;
        }

        public Uri Post(string value) => PostAsync(value).Result;

        public async Task<Uri> PostAsync(string value)
        {
            var response = await _client.PostAsJsonAsync($"{ServicesAddress}/post", value);
            response.EnsureSuccessStatusCode();  //успешно
            return response.Headers.Location;    //вернуть адрес ресурса
        }

        public HttpStatusCode Put(int id, string value) => PutAsync(id, value).Result;

        public async Task<HttpStatusCode> PutAsync(int id, string value)
        {
            var response = await _client.PutAsJsonAsync($"{ServicesAddress}/put/{id}", value);
            response.EnsureSuccessStatusCode();
            return response.StatusCode;  //статусный код
        }

        public HttpStatusCode Delete(int id) => DeleteAsync(id).Result;

        public async Task<HttpStatusCode> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"{ServicesAddress}/delete/{id}");
            return response.StatusCode;
        }
    }
}
