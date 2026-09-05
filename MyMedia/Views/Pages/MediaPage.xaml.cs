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

namespace MyMedia.Views.Pages;

/// <summary>
/// Interaction logic for MediaPage.xaml
/// </summary>
public partial class MediaPage : UserControl
{
    public MediaPage()
    {
        InitializeComponent();

        var mediaViewModel = DI.GetRequiredService<MediaViewModel>();

        DataContext = mediaViewModel;
    }
}
