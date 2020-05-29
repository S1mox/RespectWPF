using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using RespectWPF.Views;

namespace RespectWPF
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            CachingData.CurrentData.UpdateData();

            ShutdownMode = ShutdownMode.OnLastWindowClose;
        }
    }
}
