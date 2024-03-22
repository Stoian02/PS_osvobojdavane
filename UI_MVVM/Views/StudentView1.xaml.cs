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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI_MVVM.ViewModels;

namespace UI_MVVM.Views
{
    /// <summary>
    /// Interaction logic for StudentView1.xaml
    /// </summary>
    public partial class StudentView1 : UserControl
    {
        public StudentView1()
        {
            InitializeComponent();
            //Using ViewModelLocator to bind the DataContext
            DataContext = ((ViewModelLocator)Application.Current.Resources["Locator"]).StudentsViewModel;

        }
    }
}
