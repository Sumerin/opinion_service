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

namespace Administration_Panel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SiteDataGrid.AutoGenerateColumns = false;
            SiteDataGrid.CanUserAddRows = false;
            SiteDataGrid.CanUserDeleteRows = false;

            LoadSiteToView();


            UserDataGrid.AutoGenerateColumns = false;
            UserDataGrid.CanUserAddRows = false;
            UserDataGrid.CanUserDeleteRows = false;

            LoadUserDataToView();


            OpinionDataGrid.AutoGenerateColumns = false;
            OpinionDataGrid.CanUserAddRows = false;
            OpinionDataGrid.CanUserDeleteRows = false;

            LoadOpinionToView();
            
        }

        private void LoadSiteToView()
        {
            using (var ctx = new MyDbContext())
            {
                SiteDataGrid.ItemsSource = ctx.Sites.ToList();
            }
        }

        private void LoadUserDataToView()
        {
            using (var ctx = new MyDbContext())
            {
                UserDataGrid.ItemsSource = ctx.UserAccount.ToList();
            }
        }
        private void LoadOpinionToView()
        {
            using (var ctx = new MyDbContext())
            {
                OpinionDataGrid.ItemsSource = ctx.Opinions.Include("Site").ToList();
            }
        }
    }
}
