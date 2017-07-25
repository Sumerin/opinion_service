using opinion_Service.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

/*
 * TODO: Check proper Site, change name, Submit wrong site :c (SwitchOffSubmitButton)
 */
namespace Administration_Panel.ModelViewWindow.ViewModel
{
    class SiteViewModel : NotifyPropertyChanged
    {

        private string _DomainName;
        private string _ResultText;
        private bool _SubmitButtonIsEnabled;

        public string DomainName
        {
            get { return _DomainName; }
            set { _DomainName = value; OnPropertyChanged(); }
        }
        public string ResultText
        {
            get { return _ResultText; }
            private set { _ResultText = value; OnPropertyChanged(); }
        }
        public bool SubmitButtonIsEnabled
        {
            get { return _SubmitButtonIsEnabled; }
            private set { _SubmitButtonIsEnabled = true; OnPropertyChanged(); }
        }

        public ICommand Check
        {
            get { return new RelayCommand(CheckButton_Click); }
        }
        public ICommand SwitchOffSubmit
        {
            get { return new RelayCommand(SwitchOffSubmitButton); }
        }
        public ICommand Submit
        {
            get { return new RelayCommand(SubmitButton_Click); }
        }

        private Site site;
        private Window siteWindow;

        public SiteViewModel(Window siteWindow)
        {
            this.siteWindow = siteWindow;
        }
        public SiteViewModel(Window siteWindow, Site editSite) : this(siteWindow)
        {
            site = editSite;
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
                ResultText = "No Input";
                return false;
            }
            catch (Exception)
            {
                ResultText = "No Avaliable Domain";
                return false;
            }

            if (Feedback.AddressList.Length > 0)
            {
                if (!String.IsNullOrEmpty(Feedback.HostName) && !String.IsNullOrEmpty(Domain))
                {
                    ResultText = "Avaliable: " + Feedback.AddressList[0];

                    return true;
                }
            };
            ResultText = "No Avaliable Domain";
            return false;
        }
        #region Event_Handlers
        private void SubmitButton_Click(object obj)
        {
            using (var ctx = new MyDbContext())
            {
                var result = (from site in ctx.Sites
                              where this.DomainName.Equals(site.DomainName)
                              select site).FirstOrDefault();

                if (result != null)
                {
                    ResultText = "Already Exist!!";
                    return;
                }
                if (site == null)
                {
                    var newSite = new Site() { DomainName = this.DomainName };
                    ctx.Sites.Add(newSite);
                }
                else
                {
                    site.DomainName = this.DomainName;
                }
                ctx.SaveChanges();
            }
            siteWindow.Close();
        }
        private void CheckButton_Click(object obj)
        {
            ResultText = "Checking";
            bool IsDomainCorrect = IsSiteAvaliable(DomainName);
            if (IsDomainCorrect)
            {
                SubmitButtonIsEnabled = true;
            }
            else
            {
                SubmitButtonIsEnabled = false;
            }
        }
        private void SwitchOffSubmitButton(object obj)
        {
            SubmitButtonIsEnabled = false;
        }
        #endregion

    }
}
