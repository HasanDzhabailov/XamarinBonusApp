using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Real2App.Views.Pages.TabbedPages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationPage : AM.Views.Pages.CustomPage {

        private class NotificationData : System.ComponentModel.INotifyPropertyChanged {
            private string title;
            public string Title { get { return title; } set { title = value; OnPropertyChanged("Title"); } }
            private string text;
            public string Text { get { return text; } set { text = value; OnPropertyChanged("Text"); } }
            private string url;
            public string URL { get { return url; } set { url = value; OnPropertyChanged("URL"); } }
            private string date;
            public string Date { get { return date; } set { date = value; OnPropertyChanged("Date"); } }
            private bool isNew;
            public bool IsNew { get { return isNew; } set { isNew = value; OnPropertyChanged("IsNew"); } }
            private bool isLast;
            public bool IsLast { get { return isLast; } set { isLast = value; OnPropertyChanged("IsLast"); } }
            private bool isFirst;
            public bool IsFirst { get { return isFirst; } set { isFirst = value; OnPropertyChanged("IsFirst"); } }

            public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChanged(string prop = "") {
                if (PropertyChanged != null) {
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(prop));
                }
            }
        }

        private ObservableCollection<NotificationData> NotificationDatas { get; set; } = new ObservableCollection<NotificationData>();

        protected override void OnAppearing() {
            base.OnAppearing();
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();

            try {
                var task = new Task(() => {
                    foreach (var item in App.Notifications) {
                        item.IsNew = false;
                    }
                    App.Notifications.ReSaveAll();
                });
                task.Start();
            } catch (Exception ex) {
                Services.SMTP.SendException(ex);
            }
        }

        public NotificationPage() {
            try {
                this.Style = App.Current.Resources["backgroundColorPage"] as Style;
                //InitializeComponent();
                NavigationPage.SetHasNavigationBar(this, false);

                NotificationDatas.Clear();
                foreach (var row in App.Notifications) {
                    var d = new NotificationData {
                        Title = row.Title,
                        Text = row.Text,
                        URL = row.URL,
                        IsNew = row.IsNew,
                        IsFirst = row.IsFirst,
                        IsLast = row.IsLast,
                    };
                    row.OnChanged += (n) => {
                        d.Title = n.Title;
                        d.Text = n.Text;
                        d.URL = n.URL;
                        d.IsNew = n.IsNew;
                        d.IsFirst = n.IsFirst;
                        d.IsLast = n.IsLast;
                    };
                    NotificationDatas.Add(d);
                }
                App.Notifications.OnLoadEvent += () => {
                    foreach (var row in App.Notifications) {
                        var d = new NotificationData {
                            Title = row.Title,
                            Text = row.Text,
                            URL = row.URL,
                            IsNew = row.IsNew,
                            IsFirst = row.IsFirst,
                            IsLast = row.IsLast,
                        };
                        row.OnChanged += (n) => {
                            d.Title = n.Title;
                            d.Text = n.Text;
                            d.URL = n.URL;
                            d.IsNew = n.IsNew;
                            d.IsFirst = n.IsFirst;
                            d.IsLast = n.IsLast;
                        };
                        NotificationDatas.Add(d);
                    }
                };
                App.Notifications.OnPushEvent += (row) => {
                    var d = new NotificationData {
                        Title = row.Title,
                        Text = row.Text,
                        URL = row.URL,
                        IsNew = row.IsNew,
                        IsFirst = row.IsFirst,
                        IsLast = row.IsLast,
                    };
                    row.OnChanged += (n) => {
                        d.Title = n.Title;
                        d.Text = n.Text;
                        d.URL = n.URL;
                        d.IsNew = n.IsNew;
                        d.IsFirst = n.IsFirst;
                        d.IsLast = n.IsLast;
                    };
                    NotificationDatas.Insert(0, d);
                };

                var listView = new ListView(ListViewCachingStrategy.RecycleElement) {
                    BackgroundColor = Color.Transparent,
                    Margin = new Thickness(0, 0, 0, 0),
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    SeparatorVisibility = SeparatorVisibility.None,
                    SelectionMode = ListViewSelectionMode.None,
                    HasUnevenRows = true,
                    RowHeight = -1,
                    //BackgroundColor = Color.Gray,
                    ItemTemplate = new DataTemplate(() => {
                        var tap = new TapGestureRecognizer { };
                        tap.Tapped += (sender, e) => {
                            try {
                                if (sender is AM.Views.ListElement.CustomGrid obj) {
                                    if (!string.IsNullOrEmpty(obj.Element)) {
                                        var isOpen = false;
                                        try {
                                            Xamarin.Essentials.Launcher.OpenAsync(new Uri("https://" + obj.Element));
                                            isOpen = true;
                                        } catch (Exception ex) {
                                            Services.SMTP.SendException(ex);
                                            Console.WriteLine(ex.Message + ": " + obj.Element);
                                            isOpen = false;
                                        }
                                        if (!isOpen) {
                                            try {
                                                Xamarin.Essentials.Launcher.OpenAsync(new Uri("http://" + obj.Element));
                                                isOpen = true;
                                            } catch (Exception ex) {
                                                Services.SMTP.SendException(ex);
                                                Console.WriteLine(ex.Message + ": " + obj.Element);
                                                isOpen = false;
                                            }
                                        }
                                    }
                                }
                            } catch (Exception ex) {
                                Services.SMTP.SendException(ex);
                            }
                        };

                        var element = new Views.NotificationListElement {
                            Padding = new Thickness(40, 10, 40, 0),
                            Style = App.Current.Resources["notificationListElement"] as Style,
                        };
                        element.SetBindingTitle(Label.TextProperty, "Title");
                        element.SetBindingText(Span.TextProperty, "Text");
                        element.SetBindingUrl(Span.TextProperty, "URL");
                        element.SetBindingDate(Label.TextProperty, "Date");
                        element.SetBindingImageNewNotification(Image.IsVisibleProperty, "IsNew");
                        element.SetBindingTopSpace(BoxView.IsVisibleProperty, "IsLast");
                        element.SetBindingBottomSpace(BoxView.IsVisibleProperty, "IsFirst");
                        element.SetBindingGrid(AM.Views.ListElement.CustomGrid.ElementProperty, "URL");
                        element.GestureRecognizersGrid(tap);
                        element.ImageSource(ImageSource.FromResource("Real2App.Images.notificationCircle.png"));
                        element.BottomHeightRequest = AM.Platform.Display.DisplayInfo.Width <= 1080 ? 206.89f * AM.Platform.Display.GetWidthValue(1080, 1) - 106.89 : 212.12f * AM.Platform.Display.GetWidthValue(1080, 1) - 112.12f;

                        var cell = new AM.Views.Cells.ViewCell() { };
                        var sl = new StackLayout {
                            BackgroundColor = AppData.AppConfig.BackgroudColorPage,
                            Children = { element }
                        };
                        var tapsl = new TapGestureRecognizer();
                        tapsl.Tapped += (sender, e) => { };
                        sl.GestureRecognizers.Add(tap);
                        cell.View = sl;
                        return cell;
                    }),
                    //ItemsSource = App.Notifications,
                    ItemsSource = NotificationDatas,
                };
                var layout = new StackLayout {
                    BackgroundColor = Color.Transparent,
                    Children = {
                    //new Button{ Text = "Text" },
                    //new BoxView { Color = Color.Yellow },
                    listView,
                    BottomGradient
                },
                };

                try {
                    layout.Effects.Add(Effect.Resolve($"AM.{nameof(AM.Platform.SafeAreaPaddingEffect)}"));
                } catch (Exception ex) {
                    Services.SMTP.SendException(ex);
                    NLog.LogManager.GetCurrentClassLogger().Error(AM.Services.Converter.GetExceptionDetails(ex));
                }

                this.Content = layout;
            } catch (Exception ex) {
                Services.SMTP.SendException(ex);
            }
        }
    }
}