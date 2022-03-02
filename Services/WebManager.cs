using Newtonsoft.Json;
using Real2App.AppData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Real2App.Services {
    public static class WebManager {
        private static string BaseUrl { get { return "url"; } }
       
        public static AM.Web.WebAnswer GetDataPost(string number, string code) {
            var nobject = new AppData.CodeClass { PhoneNumber = number, Code = code };
            var json = JsonConvert.SerializeObject(nobject);
            return AM.Web.WebHTTP.Post(BaseUrl + "NameMethod", json/*, UserName, Password*/);
        }
		  public static AM.Web.WebAnswer GetNotifications(string date) {
            return AM.Web.WebHTTP.Get(BaseUrl + "NameMethod?Param=" + date/*, UserName, Password*/);
        }
				//........................
       

      
    }
}