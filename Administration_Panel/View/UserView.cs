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
using System.Windows.Navigation;
using System.Windows.Shapes;
using opinion_Service.Models;
using Administration_Panel.ModelViewWindow;

namespace Administration_Panel
{
    public partial class MainWindow : Window
    {
        private void LoadUserDataToView()
        {
            using (var ctx = new MyDbContext())
            {
                var executedResult = ctx.UserAccount.ToList();
                Application.Current.Dispatcher.Invoke(() => UserDataGrid.ItemsSource = executedResult);
            }
        }
        private async void LoadUserDataToViewAsync()
        {
            await Task.Factory.StartNew(LoadUserDataToView);
        }

        public void AddUserRecord()
        {
            new UserForm().Show();
        }
        private void SearchForSite_UserView(string text)
        {
            throw new NotImplementedException();
        }

        private void SearchForUser_UserView(string text)
        {
            throw new NotImplementedException();
        }
    }
}
