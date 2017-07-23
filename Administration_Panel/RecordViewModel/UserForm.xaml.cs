using opinion_Service.Models;
using opinion_Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace Administration_Panel.RecordViewModel
{
    /// <summary>
    /// Interaction logic for UserForm.xaml
    /// </summary>
    public partial class UserForm : Window
    {
        private const int MIN_PASSWD_SIZE = 8;
        public string Username { get; set; }
        public SecureString Passwd { get; set; }

        private User user;
        public UserForm()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public UserForm(User editUser)
        {
            InitializeComponent();
            this.DataContext = this;
            this.user = editUser;
            this.Username = editUser.Username;
        }
        private void SubmitBtn(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(Username) && Passwd.Length > MIN_PASSWD_SIZE)
            {
                string salt;
                string hashedPassword = Security.Encrypte(Passwd.ToString(), out salt);

                using (var ctx = new MyDbContext())
                {

                    if (user == null)
                    {
                        if (isExistingInDatabase(ctx, Username))
                        {
                            Result.Text = "Username already Taken!";
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
                        if (user.Username != this.Username && isExistingInDatabase(ctx, Username))
                        {
                            Result.Text = "Username already Taken!";
                            return;
                        }

                        user.Username = this.Username;
                        user.Salt = salt;
                        user.Password = hashedPassword;
                    }
                    ctx.SaveChanges();
                }
                Close();
            }
            else
            {
                Result.Text = "Wrong Username or Password size";
            }
        }
        private bool isExistingInDatabase(MyDbContext ctx, string username)
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

        private void SetPassword(object sender, RoutedEventArgs e)
        {
            var send = sender as PasswordBox;
            if (send != null)
            {
                this.Passwd = send.SecurePassword;
            }
        }
    }
}
