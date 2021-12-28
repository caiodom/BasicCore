using Core.Communication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.BffServices
{
    public abstract class BaseService
    {
        protected StringContent GetContent(object data)
                       => new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        protected virtual async Task<T> DeserializeObjectResponse<T>(HttpResponseMessage responseMessage)
        {


            var options = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var content = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content, options);


        }


        protected virtual bool ErrorHandlerResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest)
                return false;

            response.EnsureSuccessStatusCode();

            return true;
        }

        protected virtual ResponseResult OkReturn()
        {
            return new ResponseResult();
        }


    }
}
