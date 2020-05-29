using System.Collections.Generic;
using System.Linq;
using System.Windows;
using RespectWPF.ViewModels;
using RespectWPF.Models;

namespace RespectWPF.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Window
    {
        ServerClientViewModel server = new ServerClientViewModel();

        public RegistrationPage()
        {
            InitializeComponent();
        }

        private async void RegisterClick(object sender, RoutedEventArgs e)
        {
            CachingData.CurrentData.UpdateData();

            if (login_entry.Text != "" && login_entry.Text != null)
            {
                if (password_entry.Password == confirm_password_entry.Password)
                {
                    if (new List<User>(server.Users.Where(u => u.Login == login_entry.Text).ToList()).Count == 0)
                    {
                        CachingData.CurrentData.UpdateData();

                        int id = CachingData.CurrentData.Server.Users.Count + 1;

                        for (int i = 0; i < CachingData.CurrentData.Server.Users.Count; i++)
                        {
                            if (i != CachingData.CurrentData.Server.Users[i].Id)
                            {
                                id = i;
                                break;
                            }
                        }
                        await server.AddUser(new User()
                        {
                            Id = id,
                            Login = login_entry.Text,
                            Password = password_entry.Password,
                            Name = name_entry.Text,
                            Status = "",
                            PathToImage = ""
                        });

                        CachingData.CurrentData.UpdateData();

                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка регистрации", "Данный логин уже занят", MessageBoxButton.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка регистрации", "Поля пароля и подтверждения пароля не совпадают", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Ошибка регистрации", "Поле имени пустое пустое!", MessageBoxButton.OK);
            }
        }
    }
}
