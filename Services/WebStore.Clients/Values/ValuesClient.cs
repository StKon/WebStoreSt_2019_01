using System;
using System.Collections.Generic;
using System.Net;
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

        public Task<IEnumerable<string>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public string Get(int id) => GetAsync(id).Result;

        public Task<string> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Uri Post(string value) => PostAsync(value).Result;

        public Task<Uri> PostAsync(string value)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode Put(int id, string value) => PutAsync(id, value).Result;

        public Task<HttpStatusCode> PutAsync(int id, string value)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode Delete(int id) => DeleteAsync(id).Result;

        public Task<HttpStatusCode> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
