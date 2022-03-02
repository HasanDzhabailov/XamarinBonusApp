using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Real2App.AppData {
    [DataContract]
    public class NumberClass {
        [DataMember]
        public string PhoneNumber { get; set; }
    }
    [DataContract]
    public class TokenClass {
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
    }
    [DataContract]
    public class DateClass {
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
    }
    [DataContract]
    public class NotificationsClass {
        [DataMember]
        public string Date { get; set; }
    }
	......................................
    [DataContract]
    public class ProfileSend {
        private string phoneNumber;
        [DataMember]
        public virtual string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }
        private string surName;
        [DataMember]
        public virtual string SurName { get { return surName; } set { surName = value; } }
        private string name;
        [DataMember]
        public virtual string Name { get { return name; } set { name = value; } }
 
        private string birthday;
        [DataMember]
        public virtual string Birthday { get { return birthday; } set { birthday = value; } }
        private string gender;
        [DataMember]
        public virtual string Gender { get { return gender; } set { gender = value; } }
        private string token;
        [DataMember]
        public virtual string Token { get { return token; } set { token = value; } }

        public ProfileSend(string phoneNumber) { PhoneNumber = phoneNumber; }

        [DataMember]
        public string LoadData { get; set; }
    }
}
