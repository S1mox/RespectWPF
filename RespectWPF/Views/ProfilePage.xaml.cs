using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RespectWPF.ViewModels;
using RespectWPF.Models;

namespace RespectWPF.Views
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Window
    {
        RootsViewModel RootsViewModel { get; set; } = new RootsViewModel();

        public User User { get; set; } = new User();
        public List<Room> Rooms { get; set; } = new List<Room>();

        public ProfilePage(User currentUser)
        {
            InitializeComponent();

            User = currentUser;

            //edit_button.IsVisible = false;
            //seturi_button_uwp.IsVisible = false;

            Setup();
        }

        public ProfilePage()
        {
            InitializeComponent();
            User = CachingData.CurrentData.CurrentUser;

            Setup();
        }

        private void Setup()
        {
            //ProfileImage.SetBinding(Image.SourceProperty, new Binding() { Source = User, Path = "PathToImage" });
            //User_name.SetBinding(Label.TextProperty, new Binding() { Source = User, Path = "Name", StringFormat = "Имя: {0}", Mode = BindingMode.OneWay });
            //User_status.SetBinding(Label.TextProperty, new Binding() { Source = User, Path = "Status", StringFormat = "Статус: {0}", Mode = BindingMode.OneWay });

            //User_name_entry.SetBinding(Entry.TextProperty, new Binding() { Source = User, Path = "Name", Mode = BindingMode.TwoWay });
            //User_status_entry.SetBinding(Entry.TextProperty, new Binding() { Source = User, Path = "Status", Mode = BindingMode.TwoWay });

            //if (User != CachingData.CurrentData.CurrentUser)
            //{
            //    User_image.IsEnabled = false;
            //}

            try
            {
                CachingData.CurrentData.GetRooms(User.Id, Rooms);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{User.Id}", $"{User.Name} + {ex.Message}", MessageBoxButton.OK);
                throw;
            }

            Title = User.Login;
        }
    }
}
