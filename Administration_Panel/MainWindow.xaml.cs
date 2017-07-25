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
using Administration_Panel.Enums;

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

            InitializeSiteView();
            InitializeUserView();
            InitializeOpinionView();

        }

        private void InitializeOpinionView()
        {
            OpinionDataGrid.AutoGenerateColumns = false;
            OpinionDataGrid.CanUserAddRows = false;
            OpinionDataGrid.CanUserDeleteRows = false;
            LoadOpinionToView();
        }

        private void InitializeUserView()
        {
            UserDataGrid.AutoGenerateColumns = false;
            UserDataGrid.CanUserAddRows = false;
            UserDataGrid.CanUserDeleteRows = false;
            LoadUserDataToView();
        }

        private void InitializeSiteView()
        {
            SiteDataGrid.AutoGenerateColumns = false;
            SiteDataGrid.CanUserAddRows = false;
            SiteDataGrid.CanUserDeleteRows = false;
            LoadSiteToView();
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

        private void RefreshRecord(object sender, RoutedEventArgs e)
        {
            LoadSiteToViewAsync();
            LoadUserDataToViewAsync();
            LoadOpinionToViewAsync();
        }

        private void SearchFor(object sender, TextChangedEventArgs e)
        {
            var send = sender as TextBox;
            switch((ChoosenSearch)SearchChoose.SelectedIndex)
            {
                case ChoosenSearch.Site:
                    SearchForSite_SiteView(send.Text);
                    SearchForSite_UserView(send.Text);
                    SearchForSite_OpinionView(send.Text);
                    break;
                case ChoosenSearch.User:

                    SearchForUser_SiteView(send.Text);
                    SearchForUser_UserView(send.Text);
                    SearchForUser_OpinionView(send.Text);
                    break;
            }
        }

      
    }
}
