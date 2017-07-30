using opinion_Service.Models;
using opinion_Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Administration_Panel.ModelViewWindow.ViewModel
{
    class UserViewModel : NotifyPropertyChanged
    {

        private const int MIN_PASSWD_SIZE = 8;

        public string Username
        {
            get { return _Username; }
            set { _Username = value; OnPropertyChanged(); }
        }
        public String ResultText
        {
            get { return _ResultText; }
            set { _ResultText = value; OnPropertyChanged(); }
        }

        public ICommand Submit
        {
            get { return new RelayCommand(SubmitBtn); }
        }

        private User user;
        private Window userWindow;

        private string _Username;
        private String _ResultText;


        public UserViewModel(Window userWindow)
        {
            this.userWindow = userWindow;
        }

        public UserViewModel(Window userWindow, User user) : this(userWindow)
        {
            this.user = user;
            this.Username = user.Username;
        }

        private void SubmitBtn(object sender)
        {
            PasswordBox passwdBox = sender as PasswordBox;
            if (passwdBox != null)
            {
                if (!String.IsNullOrEmpty(Username) && passwdBox.Password.Length > MIN_PASSWD_SIZE)
                {
                    string salt;
                    string hashedPassword = Security.Encrypte(passwdBox.Password.ToString(), out salt);

                    using (var ctx = new MyDbContext())
                    {

                        if (user == null)
                        {
                            if (IsExistingInDatabase(ctx, Username))
                            {
                                ResultText = "Username already Taken!";
                                return;
                            }

                            user = new User()
                            {
                                Username = this.Username,
                                Salt = salt,
                                Password = hashedPassword
                            };
                            ctx.UserAccount.Add(user);
                        }
                        else
                        {
                            if (user.Username != this.Username && IsExistingInDatabase(ctx, Username))
                            {
                                ResultText = "Username already Taken!";
                                return;
                            }

                            user.Username = this.Username;
                            user.Salt = salt;
                            user.Password = hashedPassword;
                        }
                        ctx.SaveChanges();
                    }
                    userWindow.Close();
                }
                else
                {
                    ResultText = "Wrong Username or Password size";
                }
            }
        }
        private bool IsExistingInDatabase(MyDbContext ctx, string username)
        {
            var user = (from dbUser in ctx.UserAccount
                        where dbUser.Username.Equals(username)
                        select dbUser).FirstOrDefault();

            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
