using Administration_Panel.ModelViewWindow.ViewModel;
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

namespace Administration_Panel.ModelViewWindow
{ 
    public partial class UserForm : Window
    {
        public UserForm()
        {
            InitializeComponent();
            this.DataContext = new UserViewModel(this);
        }
        public UserForm(User editUser)
        {
            InitializeComponent();
            this.DataContext = new UserViewModel(this,editUser);
        }
        
    }
}
