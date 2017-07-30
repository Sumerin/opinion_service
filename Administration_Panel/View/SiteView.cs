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

        private void LoadSiteToView()
        {
            using (var ctx = new MyDbContext())
            {
                var executedResult = ctx.Sites.ToList();
                Application.Current.Dispatcher.Invoke(() => SiteDataGrid.ItemsSource = executedResult);
            }
        }

        private async void LoadSiteToViewAsync()
        {
            await Task.Factory.StartNew(LoadSiteToView);
        }

        private void AddSiteRecord()
        {
            new SiteForm().Show();
        }
        private void SearchForSite_SiteView(string text)
        {
                using (var ctx = new MyDbContext())
                {
                    var executedResult = (from site in ctx.Sites
                                          where site.DomainName.Contains(text)
                                          select site).ToList();
                    Application.Current.Dispatcher.Invoke(() => SiteDataGrid.ItemsSource = executedResult);
                }
        }
        private async void SearchForSite_SiteViewAsync(string text)
        {
            await Task.Factory.StartNew(() => SearchForSite_SiteView(text));
           }

    }
}
