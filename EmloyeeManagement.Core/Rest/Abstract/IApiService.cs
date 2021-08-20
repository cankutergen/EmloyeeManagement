using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmloyeeManagement.Core.Rest.Abstract
{
    public interface IApiService<TRequest, TResponse> where TResponse: class, new() where TRequest : class, new()
    {
        // Queries are added as parameter in case of custom queries

        Task<TResponse> Get(string query);

        Task<List<TResponse>> GetList(string query = "");

        Task<TResponse> Post(TRequest body);

        // Put or Patch
        Task<TResponse> Modify(string query, Method method, TRequest body);

        Task<TResponse> Delete(string query);
    }
}
