using Administration_Panel.ModelViewWindow.ViewModel;
using opinion_Service.Models;
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
using System.Windows.Shapes;

namespace Administration_Panel.ModelViewWindow
{
    /// <summary>
    /// Interaction logic for OpinionForm.xaml
    /// </summary>
    public partial class OpinionForm : Window
    {
        public OpinionForm()
        {
            InitializeComponent();
            this.DataContext = new OpinionViewModel(this);
        }
        public OpinionForm(Opinion opinion)
        {
            InitializeComponent();
            this.DataContext = new OpinionViewModel(this, opinion);

        }
    }
}
