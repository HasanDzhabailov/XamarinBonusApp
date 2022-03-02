using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Real2App.Views.Pages.TabbedPages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage {
        public ProfilePage() {
            try {
                this.Style = App.Current.Resources["backgroundColorPage"] as Style;
                //InitializeComponent();
                NavigationPage.SetHasNavigationBar(this, false);

                var nameLabel = new Label {
                    Style = App.Current.Resources["nameSurNameProfile"] as Style,
                };
                nameLabel.SetBinding(Label.TextProperty, new Binding { Source = App.Profile, Path = "Name" });
                var surnameLabel = new Label {
                    Style = App.Current.Resources["nameSurNameProfile"] as Style,
                };
                surnameLabel.SetBinding(Label.TextProperty, new Binding { Source = App.Profile, Path = "SurName" });

                var numberLabel = new Label {
                    Style = App.Current.Resources["numberProfile"] as Style,
                };
                numberLabel.SetBinding(Label.TextProperty, new Binding { Source = App.Profile, Path = "GetPhoneNumber" });

                var genderLabel = new Label {
                    Style = App.Current.Resources["dataProfile"] as Style,
                };
                genderLabel.SetBinding(Label.TextProperty, new Binding { Source = App.Profile, Path = "GetGender" });

                var dateLabel = new Label {
                    Style = App.Current.Resources["dataProfile"] as Style,
                };
                dateLabel.SetBinding(Label.TextProperty, new Binding { Source = App.Profile, Path = "GetBirthday" });

                var editButton = new AM.Views.ButtonFrame {
                    Margin = new Thickness(0, 15, 0, 0),
                    Text = "РЕДАКТИРОВАТЬ",
                    Style = App.Current.Resources["buttonActive"] as Style,
                };
                editButton.OnClick += (view, sender, e) => { App.OpenInputProfileData(); };

                var exitButton = new Button {
                    Margin = new Thickness(0, 10, 0, 0),
                    Text = "Выйти",
                    Style = App.Current.Resources["buttonOnlyText"] as Style,
                };
                exitButton.Clicked += OnExit;

                BoxView GetLine() {
                    return new BoxView { HeightRequest = 2, Color = Color.FromHex("#D4D4D4"), Margin = new Thickness(0, 10, 0, 0), };
                }

                Label GetTitleLabel(string text) {
                    return new Label {
                        Text = text,
                        Style = App.Current.Resources["headersProfile"] as Style,
                    };
                }

                this.Content = new StackLayout {
                    Padding = new Thickness(30, AM.Platform.Display.GetHeightValue(1920, 80), 30, 40),
                    Children = {
                    nameLabel,
                    surnameLabel,
                    GetLine(),
                    GetTitleLabel("Номер телефона"),
                    numberLabel,
                    GetLine(),
                    GetTitleLabel("Пол"),
                    genderLabel,
                    GetLine(),
                    GetTitleLabel("Дата рождения"),
                    dateLabel,
                    editButton,
                    exitButton,
                }
                };
            } catch (Exception ex) {
                Services.SMTP.SendException(ex);
            }
        }
        private void OnExit(object sender, EventArgs e) {
            try {
                OnExitDialog();
            } catch (Exception ex) {
                Services.SMTP.SendException(ex);
            }
        }
        private void OnExitDialog() {
            try {
                var page = new RgPopup.Real2Dialog() {
                    BackgroundInputTransparent = true,
                    //BackgroundColor = Color.Transparent,
                    HasSystemPadding = true,
                    SystemPaddingSides = Rg.Plugins.Popup.Enums.PaddingSide.All,
                };
                page.PositiveButtonClick += (sender, e) => {
                    AM.Views.Pages.RgPopup.RgPopupPage.ClosePage();
                    App.Exit();
                };
                page.NegativeButtonClick += (sender, e) => { AM.Views.Pages.RgPopup.RgPopupPage.ClosePage(); };
                AM.Views.Pages.RgPopup.RgPopupPage.OpenPage(page);
            } catch (Exception ex) {
                Services.SMTP.SendException(ex);
            }
        }
    }
}