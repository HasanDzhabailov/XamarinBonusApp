using Real2App.AppData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Real2App.Views.Pages.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedbackPage : ContentPage
    {
        string PhotoPath = null;
        public int numberOfSeconds = 300; // Количество секунд
        public TimeChecker TimeCount;
        public TimeChecker TimeOfDB;

        public FeedbackPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, true);
            if (App.Database.GetItems().Count() != 0)
            {
                TimeOfDB = App.Database.GetItem(1);
                DateTime timeNow = DateTime.Now;
                var das = timeNow - TimeOfDB.Time;
                if (das.Minutes < 5)
                {
                    buttonActivity.IsEnabled = false;
                    var tempMinute = 4 - das.Minutes;
                    var tempSeconds = 60 - das.Seconds;
                    numberOfSeconds = tempMinute * 60 + tempSeconds;
                    Device.StartTimer(TimeSpan.FromSeconds(1), OnSecondTick); //Отсчёт секунд
                }
            }

            var marginTop = AM.Platform.Display.DisplayInfo.Height < 1920 ?
                33.8 * AM.Platform.Display.GetHeightValue(1920, 1) - 21.8 : 8;

                textEditor.Style = App.Current.Resources["nameSurNameProfile"] as Style;
            //textEditor.Style = App.Current.Resources["balanceInfoMain"] as Style;
            textInput.Style = App.Current.Resources["infoDataInputData"] as Style;
            attachPhoto.Style = App.Current.Resources["buttonActive"] as Style;
            buttonActivity.Style = App.Current.Resources["buttonActive"] as Style;
            this.BindingContext = this;
        }

        async void GetPhotoAsync(object sender, EventArgs e)
        {
            try
            {
                // выбираем фото
                var photo = await MediaPicker.PickPhotoAsync();

                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                PhotoPath = newFile;// без нижних трех строк код не работает на ios
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", "Ошибка при добавлении фото \n" + ex.Message, "OK");
            }
        }

        public void RequestToMail(object view, object sender, EventArgs e)
        {
            if (textEditor.Text != null || PhotoPath != null)
            {
                if (Services.SMTP.FeedbackMail(textEditor, PhotoPath) == true)
                {
                    DateTime dateNow = DateTime.Now;
                    TimeCount = new TimeChecker();
                    TimeOfDB = new TimeChecker();
                    TimeCount.Time = dateNow;
                    buttonActivity.IsEnabled = false;
                    Device.StartTimer(TimeSpan.FromSeconds(1), OnSecondTick); //Отсчёт секунд
                    if (numberOfSeconds != 0)
                    {
                        if (App.Database.GetItems().Count() != 0)
                        {
                            TimeOfDB = App.Database.GetItem(1);
                            TimeOfDB.Time = dateNow;
                            App.Database.SaveItem(TimeOfDB);
                        }
                        else
                        {
                            App.Database.SaveNewItem(TimeOfDB);
                        }
                    }
                    DisplayAlert("Уведомление", "Спасибо за оставленный отзыв", "Ok");
                    Navigation.PushAsync(new MainPage1());
                }
            }
            else
            {
                DisplayAlert("Уведомление", "Введите текст сообщения или прикрепите фото", "Ok");
            }
        }

        private bool OnSecondTick()
        {
            if (numberOfSeconds == 0)
            {
                buttonActivity.IsEnabled = true;
                buttonActivity.Text = "Отправить";
                return false;
            }
            numberOfSeconds--;
            buttonActivity.Text = "Кнопка активна через - " + numberOfSeconds.ToString() + "сек.";

            this.BindingContext = this;
            return true;
        }
    }
}
