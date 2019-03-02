using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient
    {
        protected readonly HttpClient _client;       //клиент
        public string ServicesAddress { set; get; }  //адрес сервера

        /// <summary> Конструктор </summary>
        public BaseClient(IConfiguration configuration)
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
    }
}
