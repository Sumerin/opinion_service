using opinion_Service.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Administration_Panel.ModelViewWindow.ViewModel
{
    class OpinionViewModel : NotifyPropertyChanged
    {
        public ObservableCollection<Site> SiteCollection { get; set; }
        public int SelectedSiteIndex
        {
            get { return _SelectedSiteIndex; }
            set
            {
                _SelectedSiteIndex = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<User> UserCollection { get; set; }
        public int SelectedUserIndex
        {
            get { return _SelectedUserIndex; }
            set
            {
                _SelectedUserIndex = value;
                OnPropertyChanged();
            }
        }

        public string OpinionBody { get; set; }

        public ICommand Submit
        {
            get { return new RelayCommand(SubmitBtn); }
        }

        private Opinion opinion;
        private Window opinionWindow;

        private int _SelectedSiteIndex;
        private int _SelectedUserIndex;

        public OpinionViewModel(Window opinionWindow)
        {
            this.opinionWindow = opinionWindow;
            LoadCombboxResource();

        }
        public OpinionViewModel(Window opinionWindow, Opinion editOpinion) : this(opinionWindow)
        {
            opinion = editOpinion;
            OpinionBody = opinion.Description;
        }

        void LoadCombboxResource()
        {
            var load = new Task[2];
            load[0] = Task.Factory.StartNew(
                () =>
                {
                    using (var ctx = new MyDbContext())
                    {
                        UserCollection = new ObservableCollection<User>(ctx.UserAccount.ToList());
                    }
                });

            load[1] = Task.Factory.StartNew(
                () =>
                {
                    using (var ctx = new MyDbContext())
                    {
                        SiteCollection = new ObservableCollection<Site>(ctx.Sites.ToList());
                    }
                });
            Task.WaitAll(load);
        }

        private void SubmitBtn(object obj)
        {
            if (!String.IsNullOrEmpty(OpinionBody))
            {
                using (var ctx = new MyDbContext())
                {
                    int choosenUser = UserCollection[SelectedUserIndex].UserId;

                    int choosenSite = SiteCollection[SelectedSiteIndex].SiteId;

                    var opinionMadeBy = (from user in ctx.UserAccount
                                         where user.UserId.Equals(choosenUser)
                                         select user).FirstOrDefault();

                    var opinionAddress = (from site in ctx.Sites
                                          where site.SiteId.Equals(choosenSite)
                                          select site).FirstOrDefault();

                    if (opinion == null)
                    {

                        opinion = new Opinion()
                        {
                            Site = opinionAddress,
                            User = opinionMadeBy,
                            Description = OpinionBody
                        };
                        ctx.Opinions.Add(opinion);




                    }
                    else
                    {
                        opinion.Site = opinionAddress;
                        opinion.User = opinionMadeBy;
                        opinion.Description = OpinionBody;
                    }

                    ctx.SaveChanges();
                    opinionWindow.Close();
                }
            }
        }
    }
}
