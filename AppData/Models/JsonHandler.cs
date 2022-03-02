using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Real2App.AppData.Models
{
    [DataContract]
    public class JsonHandler
    {
        [DataMember]
        public AdvertisingCampainsData[] AdvertisingCampainsList { get; set; }
    }
    [DataContract]
    public class AdvertisingCampainsData
    {
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public AdvertisingCampains[] AdvertisingCampainsStoris { get; set; }
    }
    [DataContract]
    public class AdvertisingCampains
    {
        [DataMember]
        public string URLReal2SiteBtn { get; set; }
        [DataMember]
        public string URLTelegramBtn { get; set; }
        [DataMember]
        public string URLWhatsappBtn { get; set; }

        [DataMember]
        public string URLInstagramBtn { get; set; }
        [DataMember]
        public string Video { get; set; }
        [DataMember]
        public string Photo { get; set; }
    }
}
