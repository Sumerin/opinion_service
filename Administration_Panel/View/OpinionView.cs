﻿using System;
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

namespace Administration_Panel
{
    public partial class MainWindow : Window
    {
        private void LoadOpinionToView()
        {
            using (var ctx = new MyDbContext())
            {
                OpinionDataGrid.ItemsSource = ctx.Opinions.Include("Site").ToList();
            }
        }
        private void AddOpinionRecord()
        { }
    }
}