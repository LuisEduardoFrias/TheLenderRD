using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TheLenderRD.Domain.Dto;

namespace TheLenderRD.Presentation.ConsumeApi
{
    public class ConsumeApi
    {
        private static IConfiguration _configuration;



        private static ConsumeApi Instance;

        public static ConsumeApi GetInstance(IConfiguration configuration)
        {
            if (Instance == null)
                Instance = new ConsumeApi(configuration);

            return Instance;
        }



        private ConsumeApi(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private static Task<HttpClient> ObtenerConexion()
        {
            HttpClient _cliente = new HttpClient
            {
                BaseAddress = new Uri(_configuration.GetSection("LocalHost").Value)
            };

            _cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return Task.FromResult(_cliente);
        }


        public async ValueTask<List<T>> CallApiGETAsync<T>(string uri) where T : Error, new()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);

            using HttpClient _client = await ObtenerConexion();

            try
            {
                HttpResponseMessage response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    List<T> responseObj = JsonConvert.DeserializeObject<List<T>>(await response.Content.ReadAsStringAsync());

                    return responseObj;
                }
                else
                {
                    return new List<T>() { new T() { IsError = true, ErrorDescription = response.StatusCode.ToString() } };
                }
            }
            catch (Exception ex)
            {
                return new List<T>() { new T() { IsError = true, ErrorDescription = ex.Message.ToString() } };
            }
        }

        public async ValueTask<string> CallApiPOSTAsync<T>(string uri, T body) where T : class, new()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
            };

            using HttpClient _client = await ObtenerConexion();

            try
            {
                HttpResponseMessage response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                        return response.StatusCode.ToString();


                    var responseObj = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());

                    return responseObj;
                }
                else
                {
                    return response.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    
    }
}
