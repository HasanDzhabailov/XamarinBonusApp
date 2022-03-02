using System;
using Xamarin.Essentials;
using AM.Services;
using Xamarin.Forms;
using System.Net.Mail;
using System.IO;

namespace Real2App.Services {
    public static class SMTP {
        private const string Mail = "example@mail.ru";
        private const string Password = "12345";
        private static readonly string[] Receivers = new[] {
            "zalik-1999@mail.ru"
        };
        private static readonly string[] ReceiversForFeedback = new[] {
            "zalik-1999@mail.ru"
        };
        public static AM.Services.SMTPManager Instance { get {
                if (AM.Services.SMTPManager.Instance == null) {
                    AM.Services.SMTPManager.Init(Mail, Password, true);
                }
                return AM.Services.SMTPManager.Instance;
            }
        }

        public static bool FeedbackMail(Editor message, string PhotoPath)
        {
            // strPhoto является массивом для возможности добавления множества фото, при необходимости
            string[] strPhotoPath = new string[1];
            strPhotoPath[0] = PhotoPath;

            var phoneNumber = App.Profile.PhoneNumber;
            var name = App.Profile.Name;
            var surname = App.Profile.SurName;
            string textMail = "Текст сообщения - " + message.Text + "\n" 
                            + "Номер телефона +7" + phoneNumber + "\n" 
                            + "Имя - " + name + "\n" + "Фамилия - " + surname;
            Instance.SendAsync(ReceiversForFeedback, "", textMail, " ", strPhotoPath);
            return true;
        }

        public static void SendException(Exception ex) {
            string message = "" +
                "Тип устройства [Device type]:  " + DeviceInfo.DeviceType.ToString() + "\n" +
                "Спецификация устройства [Device idiom]:  " + DeviceInfo.Idiom.ToString() + "\n" +
                "Производитель [Device manufacturer]:  " + DeviceInfo.Manufacturer.ToString() + "\n" +
                "Модель [Device model]:  " + DeviceInfo.Model + "\n" +
                "Платформа [Device platform]:  " + DeviceInfo.Platform.ToString() + "\n" +
                "Версия [Device version]:  " + DeviceInfo.Version.ToString() + " [" + DeviceInfo.VersionString + "]\n" +
                "Имя [Device name]:  " + DeviceInfo.Name.ToString() + "\n" +
                "\n" +
                "Ширина [Device display width]:  " + DeviceDisplay.MainDisplayInfo.Width.ToString() + "\n" +
                "Высота [Device display height]:  " + DeviceDisplay.MainDisplayInfo.Height.ToString() + "\n" +
                "Плотность пикселей [Device display density]:  " + DeviceDisplay.MainDisplayInfo.Density.ToString() + "\n" +
                "Ориентация экрана [Device display orientation]:  " + DeviceDisplay.MainDisplayInfo.Orientation.ToString() + "\n" +
                "Поворот [Device display rotation]:  " + DeviceDisplay.MainDisplayInfo.Rotation.ToString() + "\n" +
                "\n" +
                "Error:\n" +
                ex.GetExceptionDetails();
            Instance.SendAsync(Receivers, "App \"TheGroupBonus\" Error!", message, true);
        }
    }
}
