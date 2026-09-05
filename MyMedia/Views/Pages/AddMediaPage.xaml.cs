using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyMedia.Helpers;
using MyMedia.ViewModels.Pages;

namespace MyMedia.Views.Pages
{
    /// <summary>
    /// Interaction logic for AddMediaPage.xaml
    /// </summary>
    public partial class AddMediaPage : UserControl
    {
        public AddMediaPage()
        {
            InitializeComponent();

            var vm = DI.GetRequiredService<AddMediaViewModel>();
            DataContext = vm;
        }
    }
}
