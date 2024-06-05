using System;
using Tarea1.Controller;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tarea1
{
    public partial class App : Application
    {
        public static DataBaseConnection DataBaseConnection { get; set; }
        public App()
        {
            InitializeComponent();
            InitializeDatabase();
            MainPage = new MainPage();
        }
        private void InitializeDatabase()
        {
            var folderApp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbPath = System.IO.Path.Combine(folderApp, "Operacionesdb.db3");
            DataBaseConnection = new DataBaseConnection(dbPath);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
