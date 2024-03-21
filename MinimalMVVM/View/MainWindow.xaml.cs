using MinimalMVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MinimalMVVM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Presenter _presenter;
        private ToLower _toLower;

        public MainWindow()
        {
            InitializeComponent();

            _presenter = new Presenter();
            _toLower = new ToLower();
            DataContext = _presenter;
        }

        private void SwitchViewModelButton(object sender, RoutedEventArgs e)
        {
            if (DataContext is Presenter)
            {
                DataContext = _toLower;
            }
            else
            {
                DataContext = _presenter;
            }
        }
    }
}
