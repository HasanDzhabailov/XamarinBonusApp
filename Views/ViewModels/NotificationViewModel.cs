using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Real2App.Views.Models {
    public class NotificationViewModel : AM.Views.ViewModel.ViewModel {
        private string badgeText;
        public string BadgeText {
            get {
                return badgeText;
            }
            set {
                badgeText = value;
                OnPropertyChanged("BadgeText");
            }
        }
    }
}
