using System.Windows;
using System.Collections.Generic;
using RespectWPF.Models;
using RespectWPF.CachingData;
using System.Linq;
using System;

namespace RespectWPF.Views
{
    public partial class LoginPage : Window
    {
        List<User> users = new List<User>();

        public LoginPage()
        {
            InitializeComponent();

            EnterToApp();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bool result = false;
                users = CurrentData.Server.Users.ToList();

                foreach (var item in users)
                {
                    if (item.Login == login_entry.Text)
                    {
                        if (password_entry.Password == item.Password)
                        {
                            CurrentData.CurrentUser = item;

                            Application.Current.Properties["user"] = item;

                            MessageBox.Show("Все верно");

                            result = true;
                        }
                    }
                }

                if (!result)
                {
                    MessageBox.Show("Авторизация", "Логин или пароль неверные", MessageBoxButton.OK); // отладка авторизации
                    password_entry.Password = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButton.OK);
                throw;
            }
        }

        private void EnterToApp()
        {
            if (Application.Current.Properties["user"] != null)
            {
                MessageBox.Show("Есть пользователь");
            }
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            RegistrationPage page = new RegistrationPage();
            page.Owner = this;

            page.Show();
            page.Closing += Page_Closing;
            Hide();
        }

        private void Page_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Show();
        }
    }
}
