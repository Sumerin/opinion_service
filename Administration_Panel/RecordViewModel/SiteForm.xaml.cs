using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using opinion_Service.Models;
using System.ComponentModel;

namespace Administration_Panel.RecordViewModel
{
    /// <summary>
    /// Interaction logic for SiteForm.xaml
    /// </summary>
    public partial class SiteForm : Window
    {
        public string DomainName { get; set; }

        public SiteForm()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private bool IsSiteAvaliable(string Domain)
        {
            IPHostEntry Feedback;

            try
            {
                Feedback = Dns.GetHostEntry(Domain);
            }
            catch (ArgumentNullException)
            {
                Result.Text = "No Input";
                return false;
            }
            catch (Exception)
            {
                Result.Text = "No Avaliable Domain";
                return false;
            }

            if (Feedback.AddressList.Length > 0)
            {
                if (!String.IsNullOrEmpty(Feedback.HostName) && !String.IsNullOrEmpty(Domain))
                {
                    Result.Text = "Avaliable: " + Feedback.AddressList[0];

                    return true;
                }
            };
            Result.Text = "No Avaliable Domain";
            return false;
        }
        #region Event_Handlers
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            using (var ctx = new MyDbContext())
            {
                var result = (from site in ctx.Sites
                              where this.DomainName.Equals(site.DomainName)
                              select site).FirstOrDefault();

                if (result != null)
                {
                    Result.Text = "Already Exist!!";
                    return;
                }

                var newSite = new Site() { DomainName = this.DomainName };
                ctx.Sites.Add(newSite);
                ctx.SaveChanges();
            }
            this.Close();
        }
        private void Check(object sender, RoutedEventArgs e)
        {
            Result.Text = "Checking";
            bool IsDomainCorrect = IsSiteAvaliable(DomainName);
            if (IsDomainCorrect)
            {
                SubmitButton.IsEnabled = true;
            }
            else
            {
                SubmitButton.IsEnabled = false;
            }
        }
        private void SwitchOffSubmitButton(object sender, KeyEventArgs e)
        {
            SubmitButton.IsEnabled = false;
        }
        #endregion  
    }
}
