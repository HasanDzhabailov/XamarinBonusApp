using Real2App.AppData.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AM.Web {
    public class WebAnswer { 
        public System.Net.HttpStatusCode StatusCode { get; private set; }
        public string Result { get; private set; }
        public Exception Exception { get; private set; }
        public WebAnswer(string result, System.Net.HttpStatusCode statusCode) { Result = result; StatusCode = statusCode; }
        public WebAnswer(Exception exception) { Exception = exception; }
    }
    public class WebHTTP {
        public static bool CheckInternetConnection(string CheckUrl) {

            try {
                System.Net.HttpWebRequest iNetRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(CheckUrl);
                iNetRequest.Timeout = 5000;
                System.Net.WebResponse iNetResponse = iNetRequest.GetResponse();
                iNetResponse.Close();

                return true;
            } catch (System.Net.WebException) {

                return false;
            }
        }

        private async static Task<WebAnswer> GetAsync(string url, params string[] parameters) {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(url);
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

           

            var client = new HttpClient();
            

            if (parameters != null && parameters.Length > 0) {
                var p = "";
                for (int i = 0; i < parameters.Length; i++) {
                    p += parameters[i];
                    if (i < parameters.Length - 1) {
                        p += ":";
                    }
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(p)));
            }

            HttpResponseMessage response = null;

            try {
                response = await client.SendAsync(request);           
                HttpContent content = response.Content;
                return new WebAnswer(await content.ReadAsStringAsync(), response.StatusCode);
            } catch (Exception ex) {
                return new WebAnswer(ex);
            }
        }

        private async static Task<WebAnswer> PostAsync(string url, string json, params string[] parameters) {

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            if (parameters != null && parameters.Length > 0) {
                var p = "";
                for (int i = 0; i < parameters.Length; i++) {
                    p += parameters[i];
                    if (i < parameters.Length - 1) {
                        p += ":";
                    }
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(p)));
            }

            HttpResponseMessage response = null;

            try {          
                response = await client.PostAsync(new Uri(url), data);
                if (response.StatusCode == System.Net.HttpStatusCode.OK) {
                }
                HttpContent content = response.Content;
                return new WebAnswer(await content.ReadAsStringAsync(), response.StatusCode);
            } catch (Exception ex) {
                return new WebAnswer(ex);
            }
        }

        public static WebAnswer Get(string url, params string[] parameters) {
            var task = Task.Run<WebAnswer>(async () => { return await GetAsync(url, parameters); });
            task.ConfigureAwait(false);
            var result = task.Result;
            if (result.Exception == null) {
                return result;
            }
            throw result.Exception;
        }

        public static JsonHandler GetForCarousel(string url, params string[] parameters)
        {
            var task = Task.Run<WebAnswer>(async () => { return await GetAsync(url, parameters); });
            task.ConfigureAwait(false);
            if (task.Result.Exception == null)
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonHandler>(task.Result.Result);
                return result;
            }
            throw task.Result.Exception;
        }
        public static WebAnswer Post(string url, string json, params string[] parameters) {
            var task = Task.Run<WebAnswer>(async () => { return await PostAsync(url, json, parameters); });
            task.ConfigureAwait(false);
            var result = task.Result;
            if (result.Exception == null) {
                return result;
            }
            throw result.Exception;
        }
    }
}
