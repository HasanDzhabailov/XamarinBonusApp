using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Real2App.AppData.Notifications {
    [DataContract]
    public class NotificationRow : AM.Platform.DataBase.ARow, INotifyPropertyChanged {
        private Action<NotificationRow> onChangeEvent;
        public event Action<NotificationRow> OnChanged { add { onChangeEvent += value; } remove { onChangeEvent -= value; } }

        [DataMember]
        public string GUID { get; set; }
        private string title;
        [DataMember]
        public string Title { get { return title; } set { title = value; OnPropertyChanged("Title"); } }
        private string text;
        [DataMember]
        public string Text { get { return text; } set { text = value; OnPropertyChanged("Text"); } }
        private string url;
        [DataMember]
        public string URL { get { return url; } set { url = value; OnPropertyChanged("URL"); } }
        private string date;
        [DataMember]
        public string Date { get { return date; } set { date = value; OnPropertyChanged("Date"); } }
        private bool isNew;
        [DataMember]
        public bool IsNew { get { return isNew; } set { isNew = value; OnPropertyChanged("IsNew"); } }
        private bool isLast;
        [DataMember]
        public bool IsLast { get { return isLast; } set { isLast = value; OnPropertyChanged("IsLast"); } }
        private bool isFirst;
        [DataMember]
        public bool IsFirst { get { return isFirst; } set { isFirst = value; OnPropertyChanged("IsFirst"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop = "") {
            onChangeEvent?.Invoke(this);
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
