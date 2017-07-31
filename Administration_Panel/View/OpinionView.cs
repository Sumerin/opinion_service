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
        private void LoadOpinionToView()
        {
            using (var ctx = new MyDbContext())
            {
                var executedResult = ctx.Opinions.Include("Site").Include("User").ToList();
                Application.Current.Dispatcher.Invoke(() => OpinionDataGrid.ItemsSource = executedResult);
            }
        }

        private async void LoadOpinionToViewAsync()
        {
            await Task.Factory.StartNew(LoadOpinionToView);
        }

        private void AddOpinionRecord()
        {
            new OpinionForm().Show();
        }

        private void SearchForUser_OpinionView(string text)
        {
            using (var ctx = new MyDbContext())
            {
                var executedResult = (from opinion in ctx.Opinions.Include("Site").Include("User")
                                      where opinion.User.Username.Contains(text)
                                      select opinion).ToList();
                Application.Current.Dispatcher.Invoke(() => OpinionDataGrid.ItemsSource = executedResult);
            }
        }

        private async void SearchForUser_OpinionViewAsync(string text)
        {
            await Task.Factory.StartNew(() => SearchForUser_OpinionView(text));
        }

        private void SearchForSite_OpinionView(string text)
        {
            using (var ctx = new MyDbContext())
            {
                var executedResult = (from opinion in ctx.Opinions.Include("Site").Include("User")
                                      where opinion.Site.DomainName.Contains(text)
                                      select opinion).ToList();
                Application.Current.Dispatcher.Invoke(() => OpinionDataGrid.ItemsSource = executedResult);
            }
        }

        private async void SearchForSite_OpinionViewAsync(string text)
        {
            await Task.Factory.StartNew(() => SearchForSite_OpinionView(text));
        }

        private void DeleteOpinionRecord()
        {
            var selected = SiteDataGrid.SelectedItem as Opinion;

            if (selected != null)
            {
                using (var ctx = new MyDbContext())
                {
                    var record = (from opinion in ctx.Opinions
                                  where opinion.OpinionId.Equals(selected.OpinionId)
                                  select opinion).FirstOrDefault();

                    ctx.Opinions.Remove(record);
                    ctx.SaveChanges();
                }
            }
        }
    }
}
