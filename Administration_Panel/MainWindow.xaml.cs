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
using System.Diagnostics;

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

        private void AddRecord(object sender, RoutedEventArgs e)
        {
            switch ((ChoosenTab)TabControl.SelectedIndex)
            {
                case ChoosenTab.SiteTab:
                    AddSiteRecord();
                    break;

                case ChoosenTab.UserTab:
                    AddUserRecord();
                    break;

                case ChoosenTab.OpinionTab:
                    AddOpinionRecord();
                    break;

                default:

                    break;
            }
        }

        private void DeleteRecord(object sender, RoutedEventArgs e)
        {

        }
    }
}
