using DataLayer.Others;
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
using UI.ViewModels;
using UI_MVVM.ViewModels;

namespace UI_MVVM.Views
{
    /// <summary>
    /// Interaction logic for LoggerList.xaml
    /// </summary>
    public partial class LoggerList : UserControl
    {
        public LoggerList()
        {
            InitializeComponent();
        }

        private void logsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid grid && grid.SelectedItem is LogEntry logEntry)
            {
                var messageBuilder = new StringBuilder();
                messageBuilder.AppendLine($"ID: {logEntry.Id}");
                messageBuilder.AppendLine($"Level: {logEntry.Level}");
                messageBuilder.AppendLine($"Message: {logEntry.Message}");
                messageBuilder.AppendLine($"User ID: {logEntry.UserId}");
                MessageBox.Show(messageBuilder.ToString(), "Log Details", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e) => ((LoggerViewModel)DataContext).FilterCommand.Execute(null);
        private void ClearFilterButton_Click(object sender, RoutedEventArgs e) => ((LoggerViewModel)DataContext).ClearFilterCommand.Execute(null);
    }
}
