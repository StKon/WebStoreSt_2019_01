using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient
    {
        protected readonly HttpClient _client;       //клиент
        public string ServicesAddress { set; get; }  //адрес сервера

        /// <summary> Конструктор </summary>
        protected BaseClient(IConfiguration configuration)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(configuration["ClientAddress"])  //адрес веб-службы
            };

            //конфигурация клиента
            _client.DefaultRequestHeaders.Clear();  //Очищаем заголовок
            //ответ в формате json
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected T Get<T>(string url) where T : new()
        {
            return GetAsync<T>(url).Result;
        }
        
        protected async Task<T> GetAsync<T>(string url, CancellationToken cancel = default(CancellationToken)) where T : new()
        {
            var list = new T();
            var response = await _client.GetAsync(url, cancel);
            if (response.IsSuccessStatusCode)
                list = await response.Content.ReadAsAsync<T>(cancel);
            return list;
        }

        protected HttpResponseMessage Post<T>(string url, T value)
        {
            return PostAsync<T>(url, value).Result;
        }

        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T value, CancellationToken cancel = default(CancellationToken))
        {
            var response = await _client.PostAsJsonAsync(url, value, cancel);
            response.EnsureSuccessStatusCode();
            return response;
        }

        protected HttpResponseMessage Put<T>(string url, T value)
        {
            return PutAsync<T>(url, value).Result;
        }

        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T value, CancellationToken cancel = default(CancellationToken))
        {
            var response = await _client.PutAsJsonAsync(url, value, cancel);
            response.EnsureSuccessStatusCode();
            return response;
        }

        protected HttpResponseMessage Delete(string url)
        {
            return DeleteAsync(url).Result;
        }

        protected async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken cancel = default(CancellationToken))
        {
            var response = await _client.DeleteAsync(url, cancel);
            return response;
        }
    }
}
