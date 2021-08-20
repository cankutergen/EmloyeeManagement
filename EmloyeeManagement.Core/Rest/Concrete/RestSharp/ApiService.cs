using EmloyeeManagement.Core.Rest.Abstract;
using EmloyeeManagement.Entities.Concrete;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmloyeeManagement.Core.Rest.Concrete.RestSharp
{
    public class ApiService<TRequest, TResponse> : IApiService<TRequest, TResponse> where TResponse : class, new() where TRequest : class, IEmployee, new()
    {
        private readonly IRestClient _restClient;
        private readonly string API_BASE_URL = ConfigurationManager.AppSettings["ApiBaseUrl"].ToString();
        private readonly string API_TOKEN = ConfigurationManager.AppSettings["ApiToken"].ToString();

        public ApiService()
        {
            _restClient = new RestClient(API_BASE_URL);
        }

        public async Task<TResponse> Get(string query)
        {
            var request = new RestRequest(query, Method.GET);
            request.AddHeader("authorization", $"Bearer {API_TOKEN}");

            var result = await _restClient.GetAsync<TResponse>(request);

            return result;
        }

        public async Task<List<TResponse>> GetList(string query)
        {
            var request = new RestRequest(query, Method.GET);
            request.AddHeader("authorization", $"Bearer {API_TOKEN}");

            var result = await _restClient.GetAsync<List<TResponse>>(request);

            return result;
        }

        public async Task<TResponse> Delete(string query)
        {
            var request = new RestRequest(query, Method.DELETE);
            request.AddHeader("authorization", $"Bearer {API_TOKEN}");

            return await _restClient.DeleteAsync<TResponse>(request);
        }

        public async Task<TResponse> Post(TRequest body)
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", $"Bearer {API_TOKEN}");
            request.AddHeader("Content-type", "application/json");

            request.AddJsonBody(new 
            {
                email = body.Email,
                name = body.Name,
                status = body.Status,
                gender = body.Gender
            });

            var response = await _restClient.ExecuteAsync<TResponse>(request);
            return response.Data;
        }

        public async Task<TResponse> Modify(string query, Method method, TRequest body)
        {
            var request = new RestRequest(query, method);
            request.AddHeader("authorization", $"Bearer {API_TOKEN}");
            request.AddHeader("Content-type", "application/json");

            request.AddJsonBody(new
            {
                id = body.Id,
                email = body.Email,
                name = body.Name,
                status = body.Status,
                gender = body.Gender
            });

            var response = await _restClient.ExecuteAsync<TResponse>(request);
            return response.Data;
        }
    }
}
