using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Real2App.AppData.Notifications {
    public class NotificationDataBase : AM.Data.StackNotify<AppData.Notifications.NotificationRow> {
        private AM.Platform.DataBase.DataBaseManager<NotificationRow> dataBaseManager;
        public AM.Platform.DataBase.DataBaseManager<NotificationRow> DataBaseManager {
            get {
                if (dataBaseManager == null) {
                    dataBaseManager = new AM.Platform.DataBase.DataBaseManager<NotificationRow>(AppData.AppConfig.GetNotificationDataBasePath);
                }
                return dataBaseManager;
            }
        }
        private event Action changeEvent;
        public event Action ChangeEvent { add { changeEvent += value; } remove { changeEvent -= value; } }
        private event Action loadEvent;
        public event Action OnLoadEvent { add { loadEvent += value; } remove { loadEvent -= value; } }
        private event Action<NotificationRow> pushEvent;
        public event Action<NotificationRow> OnPushEvent { add { pushEvent += value; } remove { pushEvent -= value; } }
        public bool IsContainNew {
            get {
                foreach (var item in this) {
                    if (item.IsNew) {
                        return true;
                    }
                }
                return false;
            }
        }
        public NotificationDataBase() { }
        public void Push(string Title, string Text, string URL, string Date, bool IsNew) {
            try {
                Push(new NotificationRow { Title = Title, Text = Text, URL = URL, Date = Date, IsNew = IsNew });
            } catch (Exception ex) { Services.SMTP.SendException(ex); }
        }
        private bool IsContainRow(NotificationRow row) {
            if (string.IsNullOrEmpty(row.GUID)) {
                return false;
            }
            foreach (var item in this) {
                if (string.IsNullOrEmpty(item.GUID)) {
                    continue;
                }
                if (item.GUID == row.GUID) {
                    return true;
                }
            }
            return false;
        }
        public override void Push(NotificationRow row) {
            try {
                if (!IsContainRow(row)) {
                    base.Push(row);
                    DataBaseManager.SaveRowAsync(row);
                    UpdatePositions();
                    pushEvent?.Invoke(row);
                    changeEvent?.Invoke();
                }
            } catch (Exception ex) { Services.SMTP.SendException(ex); }
        }
        private void UpdatePositions() {
            try {
                var i = 0;
                foreach (var row in this) {
                    row.IsFirst = false;
                    row.IsLast = false;
                    if (i == 0) {
                        row.IsLast = true;
                    } else if (i == Count - 1) {
                        row.IsFirst = true;
                    }
                    i++;
                }
            } catch (Exception ex) { Services.SMTP.SendException(ex); }
        }
        protected void AddLoad(NotificationRow row) {
            try {
                base.Push(row);
            } catch (Exception ex) { Services.SMTP.SendException(ex); }
        }
        public new bool Remove(AppData.Notifications.NotificationRow element) {
            var index = Items.IndexOf(element);
            var b = base.Remove(element);
			anager.DeleteRowAsync(Items[index]);
            UpdatePositions();
            changeEvent?.Invoke();
            return b;
        }
        public new bool RemoveAt(int index) {
            return Remove(Items[index]);
        }
        public new void RemoveItem(int index) {
            try {
                Remove(Items[index]);
            } catch (Exception ex) { Services.SMTP.SendException(ex); }
        }
        public new void Clear() {
            try {
                base.Clear();
                DataBaseManager.DeleteAllRows();
                changeEvent?.Invoke();
            } catch (Exception ex) { Services.SMTP.SendException(ex); }
        }
        public async void Load() {
            try {
                var data = await DataBaseManager.GetRowsAsync();
                foreach (var item in data) {
                    AddLoad(item);
                }
                UpdatePositions();
                changeEvent?.Invoke();
                loadEvent?.Invoke();
            } catch (Exception ex) { Services.SMTP.SendException(ex); }
        }
        public async void ReSaveAll() {
            try {
                foreach (var item in this) {
                    await DataBaseManager.SaveRowAsync(item);
                }
                changeEvent?.Invoke();
            } catch (Exception ex) { Services.SMTP.SendException(ex); }
        }
    }
}
