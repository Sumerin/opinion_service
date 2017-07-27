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
    class OpinionViewModel
    {
        public ObservableCollection<Site> SiteCollection { get; set; }
        public int SelectedSiteIndex { get; set; }

        public ObservableCollection<User> UserCollection { get; set; }
        public int SelectedUserIndex { get; set; }

        public string OpinionBody { get; set; }

        public ICommand Submit
        {
            get { return new RelayCommand(SubmitBtn); }
        }

        private Opinion opinion;
        private Window opinionWindow;
        public OpinionViewModel(Window opinionWindow)
        {
            this.opinionWindow = opinionWindow;
            LoadCombboxResource();

        }
        public OpinionViewModel(Window opinionWindow, Opinion editOpinion) : this(opinionWindow)
        {
            opinion = editOpinion;
        }
        async void LoadCombboxResource()
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

        }
    }
}
