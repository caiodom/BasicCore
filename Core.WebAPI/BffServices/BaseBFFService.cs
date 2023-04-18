using Core.WebAPI.BffServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.BffServices
{
    public class BaseBFFService<T> : BaseService, IBaseBFFService<T>
    {
        protected readonly HttpClient _httpClient;

        protected readonly string _route;
        protected BaseBFFService(HttpClient httpClient,
                                            string gateway,
                                            string route)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(gateway);
            _route = route;

        }

        public virtual async Task<IEnumerable<T>> Get()
        {
            var response = await _httpClient.GetAsync($"{this._route}");

            ErrorHandlerResponse(response);

            return await DeserializeObjectResponse<IEnumerable<T>>(response);
        }

        public virtual async Task<T> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"{this._route}/{id}");

            ErrorHandlerResponse(response);

            return await DeserializeObjectResponse<T>(response);
        }


        public virtual async Task<T> PostAsync(T entity)
        {


            var content = GetContent(entity);


            var response = await _httpClient
                                        .PostAsync($"{this._route}/", content);

            ErrorHandlerResponse(response);


            return entity;
        }


        public virtual async Task<T> PutAsync(Guid id, T entity)
        {
            var content = GetContent(entity);


            var response = await _httpClient
                                        .PutAsync($"{this._route}/{id}", content);

            ErrorHandlerResponse(response);


            return entity;
        }

        public virtual async Task<string> DeleteAsync(Guid id)
        {


            var response = await _httpClient
                                        .DeleteAsync($"{this._route}/{id}");

            ErrorHandlerResponse(response);


            return $"Removed";
        }

    }
}
